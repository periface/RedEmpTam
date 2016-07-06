(function () {
    $(document).ready(function () {
        var instance = new Engine({
            useOverlay: true,
            overlayObj: "overlay",
            autoStart: false,
            enableDebug: false,
            resolveByConvention:true
        });
        //Gets all key value props from the db
        instance.defineIterationSericePoint("Galardons", "/ViewHelpers/GetGalardons");
        instance.defineIterationSericePoint("FeaturedProducts", "/ViewHelpers/GetFeaturedProducts");
        instance.defineIterationSericePoint("FeaturesOfProduct", "/ViewHelpers/GetFeatures");
        instance.defineNewPropertyService("SimpleThemeService", "/ViewHelpers/GetPropertiesFromMain");
        instance.defineNewPropertyService("PymeInfo", "/ViewHelpers/GetPropertiesFromPymeInfo");
        instance.defineNewPropertyService("PymeContact", "/ViewHelpers/GetPropertiesFromPymeContactInfo");
        
        instance.startListener();
        //instance.getValue("SimpleThemeService", "SimpleTheme.Certifications",null,function(data) {
        //    console.log(data);
        //});
    });
})();