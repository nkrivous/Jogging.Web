function Jogging(data) {
    this.id = ko.observable("");
    this.name = ko.observable("");
    this.date = ko.observable("");
    this.time = ko.observable("");
    this.distance = ko.observable("");
    var distance = this.distance;
    var time = this.time;
    this.averageSpeed = ko.computed(function() {
        return distance() / 1000 / time() + " km/h";
    });

    app.dataModel.updateItem.call(this, data);
}

function JoggingManageViewModel(app, dataModel) {
    var self = this;

    var getUrl = "/api/joggingmanage",
        action = "jogging";

    self.joggings = ko.observableArray([]);
    self.currentJogging = ko.observable("");

    self.setCurrentJogging = function (data) {
        var jogging = new Jogging(ko.toJS(data));
        self.currentJogging(jogging);
    }

    self.resetCurrentJogging = function () {
        self.currentJogging(new Jogging());
    }

    self.removeJogging = function (data) {
        $.ajax({
            method: 'delete',
            url: getUrl + '/' + app.dataModel.userId + '/' + action + '/' + data.id(),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                self.joggings.remove(data);
            }
        });
    }

    Sammy(function () {
        this.get('#joggingmanage', function () {
            app.view(self);
            $.ajax({
                method: 'get',
                url: getUrl + '/' + app.dataModel.userId + '/' + action,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    var mappedJoggings = $.map(data, function (item) { return new Jogging(item) });
                    self.joggings(mappedJoggings);
                }
            });
        });
        this.get('#joggingmanage/userId/jogging', function () {
            app.view(self);
            $.ajax({
                method: 'get',
                url: getUrl + '/' + this.params.userId + '/' + action,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    var mappedJoggings = $.map(data, function (item) { return new Jogging(item) });
                    self.joggings(mappedJoggings);
                }
            });
        });
        this.get('#joggingmanage/userId/jogging/joggingId', function () {
            app.view(self);
            $.ajax({
                method: 'get',
                url: getUrl + '/' + this.params.userId + '/' + action,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                data: {
                    joggingId: this.params.joggingId
                },
                success: function (data) {
                    self.currentJogging(new Jogging(data));
                }
            });
        });
        this.put('#joggingmanage', function () {
            var $modal = $(this.target).closest('.modal');
            $.ajax({
                method: 'put',
                url: getUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                data: ko.toJSON(self.currentJogging),
                success: function () {
                    var editedJogging = ko.utils.arrayFirst(self.joggings(), function (x) { return x.id() === self.currentJogging().id(); });
                    var newJogging = new User(ko.toJS(self.currentJogging));
                    self.joggings.replace(editedJogging, newJogging);
                    $modal.modal('hide');
                }
            });
        });
        this.post('#joggingmanage', function () {
            var $modal = $(this.target).closest('.modal');
            $.ajax({
                method: 'post',
                url: getUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                data: ko.toJSON(self.currentJogging),
                success: function (data) {
                    self.joggings.push(new Jogging(data));
                    $modal.modal('hide');
                }
            });
        });
    });

    return self;
}

app.addViewModel({
    name: "JoggingManage",
    bindingMemberName: "joggingmanage",
    factory: JoggingManageViewModel
});
