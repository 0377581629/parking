(function () {
    $(function () {
        const _parkService = abp.services.app.parkPublic;
        const map = L.map('map-panes').setView([51.505, -0.09], 16);
        L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);
        const apiKey = "AAPK3373b61cf92b49eaa87fce9d3d4037a5dkWdLy4S9V518BcT_ebYPvh_SUzlx6AmkbdFKnaFrFnetTUR005gMS083ONDA0yU";
        const geocoder = L.esri.Geocoding.geocodeService({apikey: apiKey});
        let mapLocations = [];
        
        let btnViewInMaps = $('.btnViewInMap');
        btnViewInMaps.each(function () {
            let isHeadOffice = $(this).attr('is-head');
            let isHead = isHeadOffice !== undefined && isHeadOffice !== null && isHeadOffice === 'true';
            let markerTitle = $(this).attr('marker-title');
            let address = $(this).attr('address');

            if (address !== undefined && address !== null && address.length > 0) {
                geocoder
                    .geocode()
                    .text(address)
                    .run((error, result) => {
                        if (error) {
                            return;
                        }
                        if (result !== undefined && result !== null && result.results !== undefined && result.results !== null && result.results.length > 0) {
                            let location = result.results[0];
                            let marker = L.marker(location.latlng)
                                .addTo(map)
                                .bindPopup(`<h3>${markerTitle}</h3><p>${location.properties.Match_addr}</p>`);
                            if (isHead)
                                marker.openPopup();
                            map.setView(location.latlng, 16);
                            mapLocations.push({
                                title: markerTitle,
                                isHead: isHead,
                                address: address,
                                latlng: location.latlng,
                                marker: marker
                            });
                        }
                    });
            }
        });
        btnViewInMaps.on('click', function () {
            let markerTitle = $(this).attr('marker-title');
            if (mapLocations.length > 0) {
                for (let i = 0; i < mapLocations.length; i++) {
                    if (mapLocations[i].title === markerTitle) {
                        map.setView(mapLocations[i].latlng, 16);
                        mapLocations[i].marker.openPopup();
                    }
                }
            }
        });

        let _$UserContactInformationForm = $('#contact-form');

        let btnSave = $('#ContactSubmit');
        if (btnSave) {
            btnSave.on('click', function () {
                if (!_$UserContactInformationForm.valid()) {
                    return;
                }
                let userContact = {
                    code: $("#UserContact_Code").val(),
                    name: $("#UserContact_Name").val(),
                    email: $("#UserContact_Email").val(),
                    phone: $("#UserContact_Phone").val(),
                    title: $("#UserContact_Title").val(),
                    content: $("#UserContact_Content").val(),
                    isActive: true
                };

                _parkService.getUserContact(userContact).done(function () {
                    abp.message.success(app.localize('FB_SendSuccess'));
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                }).always(function () {
                });
            });
        }
    });
})();


