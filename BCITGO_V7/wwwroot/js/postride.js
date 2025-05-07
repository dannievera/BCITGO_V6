//// Load Photon JS Library dynamically
//var photonScript = document.createElement('script');
//photonScript.src = "https://unpkg.com/@maptiler/photon@1.1.2/dist/photon.min.js";
//document.head.appendChild(photonScript);

//// When Photon library loaded → initialize autocomplete
//photonScript.onload = function () {
//    setupAutocomplete('StartLocation');
//    setupAutocomplete('EndLocation');
//};

//// Autocomplete Function
//function setupAutocomplete(inputId) {
//    var input = document.getElementById(inputId);
//    var photonControl = new Photon.photon(input, {
//        placeholder: 'Type a location...',
//        limit: 5,
//        lang: 'en',
//        position: true
//    });

//    photonControl.on('selected', function (result) {
//        input.value = result.properties.name + (result.properties.city ? ', ' + result.properties.city : '');
//    });
//}

//// Wizard Navigation
//function nextStep() {
//    document.getElementById('step1').classList.remove('active');
//    document.getElementById('step2').classList.add('active');

//    document.getElementById('stepIndicator1').classList.remove('active-step');
//    document.getElementById('stepIndicator2').classList.add('active-step');
//}

//function prevStep() {
//    document.getElementById('step2').classList.remove('active');
//    document.getElementById('step1').classList.add('active');

//    document.getElementById('stepIndicator2').classList.remove('active-step');
//    document.getElementById('stepIndicator1').classList.add('active-step');
//}
