(function ($) {

    //Notification handler
    abp.event.on('abp.notifications.received', function (userNotification) {
        abp.notifications.showUiNotifyForUserNotification(userNotification);
    });

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };
    function save(formData, url) {
        return abp.ajax({
            url: url,
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: "POST"
        });
    };
    window.postWithUpload = function (element,formData,url, callback) {
        abp.ui.setBusy(element, save(formData, url).done(function(d) {
            callback(d);
        }));
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }

})(jQuery);