import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class StorageService {
  private ROOT_KEY = "MYKEY";
  private obj = {};

  constructor() {}

  private getByRoot() {
    const data = localStorage.getItem(this.ROOT_KEY);
    return data ? JSON.parse(data) : {};
  }

  setItem(key: any, value: any) {
    const data = this.getByRoot();
    data[key] = value;
    localStorage.setItem(this.ROOT_KEY, JSON.stringify(data));
  }

  getItem(key: string) {
    const data = this.getByRoot();

    if (data.hasOwnProperty(key)) {
      if (key === "menus" || key === "roles") {
        return JSON.parse(data[key]);
      } else {
        return data[key];
      }
    } else {
      return null;
    }
  }

  removeItem(key: string) {
    const data = this.getByRoot();
    if (data.hasOwnProperty(key)) {
      delete data[key];
      localStorage.setItem(this.ROOT_KEY, JSON.stringify(data));
    }
  }

  reset() {
    localStorage.removeItem(this.ROOT_KEY);
  }
}
