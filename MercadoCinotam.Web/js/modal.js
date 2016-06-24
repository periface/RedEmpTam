(function ($) {
    var modal = function (container) {
        var modalTypes = {
            MODAL_CANCEL: 'MODAL_CANCEL'
        }
        var modalConfig = {
            show: true,
            backdrop: 'static',
            keyboard: false
        }
        var modalInstance = {};
        modalInstance.modalCloseEvent = {
        };
        var selfModal = this;
        if (container) {
            var isJquery = container instanceof $;
            if (isJquery) {
                selfModal.container = container;
            } else {
                selfModal.container = $(container);
            }
        } else {
            console.warn("Modal not defined loading default");
            selfModal.container = $('#modal');
        }

        selfModal.initModal = function () {
            selfModal.container.modal(modalConfig);
        }
        modalInstance.open = function (url, data) {
            if (url) {
                abp.ui.setBusy("body", function() {
                    return selfModal.container.load(url, data, function() {
                        selfModal.initModal();
                    });
                });
            }
        }
        modalInstance.close = function (data, modalType) {
            abp.event.trigger(modalType, data);
            selfModal.container.modal('hide');
        }
        function initListener() {
            console.log('Modal service beep awaiting orders... bep bep');
            $('body').on('click', '[data-modal]', function (e) {
                e.preventDefault();
                var url = $(this).data('url') || $(this).attr('href');
                console.log(url);
                if (url) {
                    selfModal.container.load(url, function () {
                        selfModal.initModal();
                    });
                }
            });
            $('body').on('click', '[data-cancel]', function (e) {
                e.preventDefault();
                modalInstance.close({
                }, modalTypes.MODAL_CANCEL);
            });
        }
        initListener();
        return modalInstance;
    };
    var nameSpace = abp.utils.createNamespace(abp, 'app.bootstrap');
    nameSpace.modal = modal;
})($);