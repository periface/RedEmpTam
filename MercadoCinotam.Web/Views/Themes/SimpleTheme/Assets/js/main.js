(function () {
    $(document).ready(function () {
        var instance = new Engine({
            useOverlay: true,
            overlayObj: "overlay",
            autoStart: false,
            enableDebug: true,
            resolveByConvention:true
        });
        //Gets all key value props from the db
        instance.defineNewPropertyService("SimpleThemeService", "/ViewHelpers/GetPropertiesFromMain");
        instance.defineNewPropertyService("PymeInfo", "/ViewHelpers/GetPropertiesFromPymeInfo");
        instance.defineNewPropertyService("PymeContact", "/ViewHelpers/GetPropertiesFromPymeContactInfo");
        instance.startListener();
        instance.getValue("SimpleThemeService", "SimpleTheme.Certifications",null,function(data) {
            console.log(data);
        });
    });
})();