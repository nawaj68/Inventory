using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Core.Extensions
{
    public static class SqlExtention
    {
        public static SqlParameter ToSqlParameter<T>(this T? value, string parameterName) where T : struct
        {
            return value.Equals(null) ? new SqlParameter(parameterName, DBNull.Value) : new SqlParameter(parameterName, value);
        }

        public static SqlParameter ToSqlParameter<T>(this T value, string parameterName) where T : IComparable
        {
            return value == null ? new SqlParameter(parameterName, DBNull.Value) : new SqlParameter(parameterName, value);
        }

        public static SqlParameter ToSqlParameter(this DataTable value, string parameterName, string typeName)
        {
            SqlParameter sqlParameter = null;
            if (value == null)
            {
                sqlParameter = new SqlParameter()
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = parameterName,
                    TypeName = typeName
                };
                sqlParameter.Value = DBNull.Value;
            }
            else
            {
                sqlParameter = new SqlParameter()
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = parameterName,
                    TypeName = typeName
                };
                sqlParameter.Value = value;
            }

            return sqlParameter;
        }

        public static DataTable ToDataTable<T>(this List<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }

        public static Task<T> MapToEntity<T>(this DataSet dataSet) where T : class, new()
        {
            try
            {
                return Task.Factory.StartNew<T>(() =>
                {
                    var entity = new T();
                    var propertyInfos = entity
                        .GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(prop => !Attribute.IsDefined(prop, typeof(IgnorePropAttribute))).ToList();

                    propertyInfos.Select((value, index) => new { index, prop = value }).ToList().ForEach(x =>
                    {
                        if (x.prop.PropertyType.IsGenericType && x.prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var list = (System.Collections.IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(x.prop.PropertyType.GetGenericArguments()[0]));
                            foreach (DataRow row in dataSet.Tables?[x.index].Rows)
                            {
                                var instance = Activator.CreateInstance(x.prop.PropertyType.GetGenericArguments()[0]);
                                instance.GetType().GetProperties().ToList().ForEach(prop =>
                                {
                                    prop.SetValue(instance, row[prop.Name].GetType() == typeof(DBNull) ? null : row[prop.Name]);
                                });
                                list.Add(instance);
                            }
                            x.prop.SetValue(entity, list);
                        }
                    });

                    return entity;
                });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public static string ToDb2InsertQuery<T>(this List<T> data, string tableName)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
            StringBuilder query = new StringBuilder();
            query.Append($"INSERT INTO {tableName} (");
            int entityCount = 1;
            foreach (var entity in data)
            {
                values.Clear();
                foreach (var item in entity.GetType().GetProperties())
                {
                    values.Add(new KeyValuePair<string, string>(item.Name, item.GetValue(entity).ToString()));
                }

                if(entityCount == 1)
                {
                    int valueCount = 1;
                    foreach (var item in values)
                    {
                        query.Append(item.Key);
                        if (valueCount < values.Count)
                            query.Append(", ");

                        valueCount++;
                    }
                    query.Append(")");
                }

                query.Append(" SELECT ");
                int itemCount = 1;
                foreach (var item in values)
                {
                    if (item.Key.GetType().Name == "String")
                    {
                        if (string.IsNullOrEmpty(item.Value))
                            query.Append("''");
                        else
                            query.Append($"'{item.Value}'");
                    }
                    else
                        query.Append(item.Value);

                    if (itemCount < values.Count)
                        query.Append(", ");

                    itemCount++;
                }
                query.Append(" FROM SYSIBM.SYSDUMMY1");

                if (entityCount < data.Count)
                    query.Append(" UNION ALL");

                entityCount++;
            }

            return query.ToString();
        }
    }
}
