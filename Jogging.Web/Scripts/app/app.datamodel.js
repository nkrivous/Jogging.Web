function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/";
    self.siteUrl = "/";

    // Route operations

    // Other private operations

    // Operations
    self.updateItem = function (data) {
        for (var i in data) {
            var property = i.toLowerCase();
            if (ko.isObservable(this[property]) && (!ko.isComputed(this[property]) || this[property].hasWriteFunction)) {
                this[property](data[i]);
            }
        }
    }

    self.user = ko.observable("");

    // Data
    self.returnUrl = self.siteUrl;

    // Data access operations
    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
    };

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };
}
