function LoginViewModel() {
    this.email = ko.observable("");
    this.password = ko.observable("");
}

function HomeViewModel(app, dataModel) {
    var self = this;

    self.isRegisterForm = ko.observable(false);
    self.login = ko.observable(new LoginViewModel());
    self.register = ko.observable(new User());
    self.isAuthenticated = ko.computed(function () { return app.dataModel.user() !== ""; });

    self.userLogin = function() {

        $.ajax({
            method: 'post',
            url: '/home/login',
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON(self.login),
            success: function (data) {
                if (data !== "") {
                    app.dataModel.user(new User(ko.toJS(data)));
                } else {
                    app.dataModel.user("");
                    self.showLoginForm();
                }
            }
        });

        return false;
    };

    self.userLogout = function() {

        $.ajax({
            method: 'post',
            url: '/home/logout',
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON(app.dataModel.user().id),
            success: function() {
                app.dataModel.user("");
                self.showLoginForm();
            }
        });

        return false;
    };

    self.showRegisterForm = function() {
        self.isRegisterForm(true);
    };

    self.showLoginForm = function() {
        self.isRegisterForm(false);
    };

    Sammy(function () {
        this.get('#home', function () {
            app.view(self);

            $.ajax({
                method: 'get',
                url: app.dataModel.userInfoUrl,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //set user id and role
                }
            });
        });

        this.get('/', function () { this.app.runRoute('get', '#home') });
    });

    return self;
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
