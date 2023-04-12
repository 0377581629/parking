var GroupInputType = (function () {
        return function () {
            const _dynamicPropertyAppService = abp.services.app.dynamicProperty;
            const _dynamicPropertyValueAppService = abp.services.app.dynamicPropertyValue;
            let _options;

            function init(inputTypeInfo, options) {
                _options = options;
            }

            let $groupInputWrapper;
            let allProperties;
            let allPropertiesIds = [];
            let dataAndInputTypes = [];

            function getView(selectedValues, allItems) {

                if (selectedValues === null || selectedValues === undefined)
                    selectedValues = [];
                let separateSelectedValues = [];
                if (selectedValues.length > 0)
                    separateSelectedValues = jQuery.parseJSON(selectedValues[0]);
                $groupInputWrapper = $('<div></div>');
                $.when(_dynamicPropertyAppService
                    .getAll({})
                    .done(function (res) {
                        allProperties = res.items;
                        allProperties = $.grep(allProperties, function (property, index) {
                            return allItems.indexOf(property.propertyName) >= 0;
                        });
                        if (allProperties && allProperties.length > 0) {
                            $.each(allProperties, function (index, property) {
                                allPropertiesIds.push(property.id);
                            });
                        }
                    })
                    .then(function () {
                            _dynamicPropertyValueAppService
                                .getAllValuesOfDynamicProperties({ids: allPropertiesIds})
                                .done(function (res) {
                                    for (let i = 0; i < allProperties.length; i++) {
                                        let targetProperty = allProperties[i];
                                        let targetPropertySelectedValues;
                                        if (separateSelectedValues && separateSelectedValues.length > 0) {
                                            for (let i = 0; i < separateSelectedValues.length; i++) {
                                                if (separateSelectedValues[i].dynamicEntityPropertyId === targetProperty.id) {
                                                    targetPropertySelectedValues = separateSelectedValues[i].values;
                                                    break;
                                                }
                                            }
                                        }

                                        const inputTypeManager = abp.inputTypeProviders.getInputTypeInstance({
                                            inputType: {name: targetProperty.inputType}
                                        });

                                        dataAndInputTypes.push({
                                            data: targetProperty,
                                            inputTypeManager: inputTypeManager,
                                            childWrapperId: ''
                                        });

                                        let allValuesInputTypeHas = $.grep(res.items, function (propertyValue, index) {
                                            return propertyValue.dynamicPropertyId === targetProperty.id;
                                        });

                                        const propertyView = inputTypeManager.getView(targetPropertySelectedValues, allValuesInputTypeHas);

                                        let $propertyInputWrapper = $("<div class='form-group mb-5px'></div>");
                                        let propertyName = $("<label></label>").text(targetProperty.displayName);
                                        $propertyInputWrapper
                                            .append(propertyName)
                                            .append(propertyView);

                                        $groupInputWrapper.append($propertyInputWrapper);
                                        inputTypeManager.afterViewInitialized();
                                    }
                                })
                                .then(function () {
                                    if (_options.multiChild && separateSelectedValues.length > 0) {
                                        let childWrapperIds = [];
                                        for (let i = 0; i < separateSelectedValues.length; i++) {
                                            if (separateSelectedValues[i].childWrapperId !== undefined &&
                                                separateSelectedValues[i].childWrapperId !== '' &&
                                                childWrapperIds.indexOf(separateSelectedValues[i].childWrapperId) < 0) {
                                                childWrapperIds.push(separateSelectedValues[i].childWrapperId);

                                                let childWrapperId = separateSelectedValues[i].childWrapperId;
                                                
                                                $.when(
                                                    _dynamicPropertyValueAppService
                                                        .getAllValuesOfDynamicProperties({ids: allPropertiesIds})
                                                        .done(function (res) {
                                                            let $childGroupInputWrapper = $('<div id="' + childWrapperId + '" style="margin: 10px 0; padding: 10px; border: 1px solid #dddddd; border-radius: 5px"></div>');
                                                            const btnDelete = $("<button class=\"btn btn-sm btn-clean btn-icon btn-icon-md mx-0 float-right\"></button>")
                                                                .click(function () {
                                                                    removeChild(childWrapperId);
                                                                });
                                                            const deleteIcon = $("<i class=\"la la-trash text-danger\"></i>");
                                                            deleteIcon.appendTo(btnDelete);
                                                            $childGroupInputWrapper.append(btnDelete);

                                                            for (let i = 0; i < allProperties.length; i++) {
                                                                let targetProperty = allProperties[i];

                                                                const inputTypeManager = abp.inputTypeProviders.getInputTypeInstance({
                                                                    inputType: {name: targetProperty.inputType}
                                                                });

                                                                dataAndInputTypes.push({
                                                                    data: targetProperty,
                                                                    inputTypeManager: inputTypeManager,
                                                                    childWrapperId: childWrapperId
                                                                });

                                                                let allValuesInputTypeHas = $.grep(res.items, function (propertyValue, index) {
                                                                    return propertyValue.dynamicPropertyId === targetProperty.id;
                                                                });

                                                                let propertyValue = [];
                                                                if (separateSelectedValues !== null && separateSelectedValues.length > 0) {
                                                                    for (let i = 0; i < separateSelectedValues.length; i++) {
                                                                        if (separateSelectedValues[i].childWrapperId !== undefined &&
                                                                            separateSelectedValues[i].childWrapperId !== '' &&
                                                                            separateSelectedValues[i].childWrapperId === childWrapperId &&
                                                                            separateSelectedValues[i].dynamicEntityPropertyId === targetProperty.id) {
                                                                            propertyValue = separateSelectedValues[i].values;
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                const propertyView = inputTypeManager.getView(propertyValue, allValuesInputTypeHas);

                                                                let $propertyInputWrapper = $("<div class='form-group mb-5px'></div>");
                                                                let propertyName = $("<label></label>").text(targetProperty.displayName);
                                                                $propertyInputWrapper
                                                                    .append(propertyName)
                                                                    .append(propertyView);
                                                                $childGroupInputWrapper.append($propertyInputWrapper);
                                                                inputTypeManager.afterViewInitialized();
                                                            }
                                                            $groupInputWrapper.append($childGroupInputWrapper);
                                                        })
                                                        .then(function(){})
                                                ).then(function () {
                                                });
                                            }
                                        }
                                    }
                                });
                        }
                    )
                ).done(function () {
                });
                return $groupInputWrapper[0];
            }

            function addChild(childWrapperId = '', separateSelectedValues = null) {
                if (childWrapperId === '')
                    childWrapperId = app.normalGuid();
                _dynamicPropertyValueAppService
                    .getAllValuesOfDynamicProperties({ids: allPropertiesIds})
                    .done(function (res) {
                        let $childGroupInputWrapper = $('<div id="' + childWrapperId + '" style="margin: 10px 0; padding: 10px; border: 1px solid #dddddd; border-radius: 5px"></div>');
                        const btnDelete = $("<button class=\"btn btn-sm btn-clean btn-icon btn-icon-md mx-0 float-right\"></button>")
                            .click(function () {
                                removeChild(childWrapperId);
                            });
                        const deleteIcon = $("<i class=\"la la-trash text-danger\"></i>");
                        deleteIcon.appendTo(btnDelete);
                        $childGroupInputWrapper.append(btnDelete);

                        for (let i = 0; i < allProperties.length; i++) {
                            let targetProperty = allProperties[i];

                            const inputTypeManager = abp.inputTypeProviders.getInputTypeInstance({
                                inputType: {name: targetProperty.inputType}
                            });

                            dataAndInputTypes.push({
                                data: targetProperty,
                                inputTypeManager: inputTypeManager,
                                childWrapperId: childWrapperId
                            });

                            let allValuesInputTypeHas = $.grep(res.items, function (propertyValue, index) {
                                return propertyValue.dynamicPropertyId === targetProperty.id;
                            });

                            let propertyValue = [];
                            if (separateSelectedValues !== null && separateSelectedValues.length > 0) {
                                for (let i = 0; i < separateSelectedValues.length; i++) {
                                    if (separateSelectedValues[i].childWrapperId !== undefined &&
                                        separateSelectedValues[i].childWrapperId !== '' &&
                                        separateSelectedValues[i].childWrapperId === childWrapperId &&
                                        separateSelectedValues[i].dynamicEntityPropertyId === targetProperty.id) {
                                        propertyValue = separateSelectedValues[i].values;
                                        break;
                                    }
                                }
                            }
                            const propertyView = inputTypeManager.getView(propertyValue, allValuesInputTypeHas);

                            let $propertyInputWrapper = $("<div class='form-group mb-5px'></div>");
                            let propertyName = $("<label></label>").text(targetProperty.displayName);
                            $propertyInputWrapper
                                .append(propertyName)
                                .append(propertyView);
                            $childGroupInputWrapper.append($propertyInputWrapper);
                            inputTypeManager.afterViewInitialized();
                        }
                        $groupInputWrapper.append($childGroupInputWrapper);
                    });
            }

            function removeChild(wrapperId) {
                $groupInputWrapper.find('#' + wrapperId).remove();
                if (dataAndInputTypes) {
                    dataAndInputTypes = $.grep(dataAndInputTypes, function (inputType, index) {
                        return inputType.childWrapperId !== wrapperId;
                    });
                }
            }

            function getSelectedValues() {
                if (!dataAndInputTypes) {
                    return '';
                }
                const newValues = [];
                for (let i = 0; i < dataAndInputTypes.length; i++) {
                    newValues.push({
                        dynamicEntityPropertyId: dataAndInputTypes[i].data.id,
                        values: dataAndInputTypes[i].inputTypeManager.getSelectedValues(),
                        childWrapperId: dataAndInputTypes[i].childWrapperId
                    });
                }
                return [JSON.stringify(newValues)];
            }

            function afterViewInitialized() {

            }

            return {
                name: "GROUP_INPUT_TYPE",
                init: init,
                getSelectedValues: getSelectedValues,
                getView: getView,
                addChild: addChild,
                hasValues: true,//is that input type need values to work. For example dropdown need values to select.
                afterViewInitialized: afterViewInitialized
            };
        };
    }
)
();

(function () {
    var GroupInputTypeProvider = new function () {
        this.name = "GROUP_INPUT_TYPE";
        this.get = function () {
            return new GroupInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(GroupInputTypeProvider);
})();