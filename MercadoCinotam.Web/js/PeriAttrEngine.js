var Engine = (function (options) {
    if (!options) {
        options = {
            useOverlay: true,
            autoStart: false,
            overlayObj: undefined
        }
    }
    var tagType = {
        IMG: "img"
    }
    var self = this;

    //All property services defined
    this.propertyServices = [];

    //Define a new property service

    //Endpoint : Server Side Key, Value
    this.definePropertyService = function (propertyServiceName, propertyServiceEndPoint) {
        self.propertyServices.push({
            propertyServiceName: propertyServiceName,
            propertyServiceEndPoint: propertyServiceEndPoint
        });
    }
    function findElement(arr, propName, propValue) {
        for (var i = 0; i < arr.length; i++)
            if (arr[i][propName] === propValue)
                return arr[i];

        // will return undefined if not found; you could return a default instead
    }
    //Listener to data-* properties
    this.listener = function () {
        //Lets try http://stackoverflow.com/questions/5627284/pass-in-an-array-of-deferreds-to-when
        var deferred = [];
        $("[data-property]").each(function () {
            var element = $(this);
            var propertyRequest = $(this).data("property");
            var properyServiceName = $(this).data("servicename");

            var serviceInfo = findElement(self.propertyServices, "propertyServiceName", properyServiceName);
            console.log(serviceInfo);
            if (serviceInfo == undefined) {
                console.error("Servicio no definido");
            } else {

                self.buildAjaxObj(serviceInfo.propertyServiceEndPoint, propertyRequest, element, deferred);
            }

        });
        $.when.apply($, deferred).done(self.allDoneFunction);
        //It works!! such awesome!! much power, very async
    };
    if (options.autoStart) {
        self.listener();
    }
    this.buildAjaxObj = function (endPoint, name, $element, deferredArray) {
        if (endPoint !== "" && name !== "") {
            var endPointUrl = endPoint + "?key=" + name;
            deferredArray.push($.ajax({
                url: endPointUrl,
                data: name,
                success: function (data, textStatus, jqXhr) {
                    self.bindData($element, data);
                }
            }));
        }
    }

    this.allDoneFunction = function () {
        if (options.useOverlay) {
            if (options.overlayObj) {
                document.getElementById(options.overlayObj).style.width = "0%";
            } else {
                console.warn("No hay una capa de carga definida");
            }
        }
        console.log("All request done");
    }
    this.bindData = function (element, data) {
        var elementTag = element[0].nodeName.toLowerCase();
        switch (elementTag) {
            case tagType.IMG:
                element.attr("src", data);
                break;
            default:
                element.text(data);
                break;
        }
    }
    return {
        propertyServices: this.propertyServices,
        defineNewPropertyService: this.definePropertyService,
        startListener: this.listener
    };
});
