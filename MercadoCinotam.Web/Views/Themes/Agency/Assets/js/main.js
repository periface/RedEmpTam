var instance = new Engine({
    useOverlay: true,
    overlayObj: "overlay",
    autoStart: false
});

instance.defineNewPropertyService("SimpleThemeService", "/ViewHelpers/GetPropertiesFromMain");
instance.defineNewPropertyService("PymeInfo", "/ViewHelpers/GetPropertiesFromPymeInfo");
instance.defineNewPropertyService("PymeContact", "/ViewHelpers/GetPropertiesFromPymeContactInfo");
instance.startListener();