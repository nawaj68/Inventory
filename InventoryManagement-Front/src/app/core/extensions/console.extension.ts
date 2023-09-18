export class ConsoleExtension {
  static clearConsole(): void {
    if (window) {
      window.console.log = function () {};
    }
  }

  static clearConsolePolyfil(): void {
    if (!window.console) {
      var console = {
        log: function () {},
        warn: function () {},
        error: function () {},
        time: function () {},
        timeEnd: function () {},
      };

      // console.warn = () => {};
    }
  }
}
