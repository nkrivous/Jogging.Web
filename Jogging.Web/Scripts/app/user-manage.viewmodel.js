function User(data) {
    this.id = ko.observable("");
    this.name = ko.observable("");
    this.role = ko.observable("");
    this.email = ko.observable("");
    this.password = ko.observable("");

    app.dataModel.updateItem.call(this, data);
}

function UserManageViewModel(app, dataModel) {
    var self = this;
    var getUrl = "/api/usermanage";

    self.users = ko.observableArray([]);
    self.currentUser = ko.observable();

    self.setCurrentUser = function (data) {
        var user = new User(ko.toJS(data));
        self.currentUser(user);
    }

    self.resetCurrentUser = function() {
        self.currentUser(new User());
    }

    self.removeUser = function (data) {
        $.ajax({
            method: 'delete',
            url: getUrl + '/' + data.id(),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function () {
                self.users.remove(data);
            }
        });
    }

    Sammy(function() {
        this.get('#usermanage', function() {
            app.view(self);
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: getUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function(data) {
                    var mappedUsers = $.map(data, function(item) { return new User(item) });
                    self.users(mappedUsers);
                }
            });
        });
        this.get('#usermanage/userId', function() {
            app.view(self);
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: self.getUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                data: {
                    id: this.params.userId
                },
                success: function(data) {
                    self.currentUser(new User(data));
                }
            });
        });
        this.put('#usermanage', function() {
            var $modal = $(this.target).closest('.modal');
            $.ajax({
                method: 'put',
                url: getUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                data: ko.toJSON(self.currentUser),
                success: function () {
                    var editedUser = ko.utils.arrayFirst(self.users(), function (x) { return x.id() === self.currentUser().id(); });
                    var newUser = new User(ko.toJS(self.currentUser));
                    self.users.replace(editedUser, newUser);
                    $modal.modal('hide');
                }
            });
        });
        this.post('#usermanage', function () {
            var $modal = $(this.target).closest('.modal');
            $.ajax({
                method: 'post',
                url: getUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                data: ko.toJSON(self.currentUser),
                success: function (data) {
                    self.users.push(new User(data));
                    $modal.modal('hide');
                }
            });
        });
    });

    return self;
}

app.addViewModel({
    name: "UserManage",
    bindingMemberName: "usermanage",
    factory: UserManageViewModel
});
