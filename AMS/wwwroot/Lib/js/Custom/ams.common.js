var ams = ams || {};

ams.common = new function () {
    this.HistoryBack = function () {
        window.history.back()
    }

    this.AjaxRequest = function (methodType,  reqData) {
        return new Promise((resolve, reject) => {
            //ShowLoader(); // Start the loader
            $.ajax({
                type: methodType,
                //url: UrlContent(),
                data: reqData,
                success: function (response) {
                    //HideLoader(); // Hide the loader
                    resolve(response);
                },
                error: function (xhr, status, error) {
                    //HideLoader(); // Hide the loader
                    reject({ xhr, status, error });
                }
            });
        });
    };

    this.AjaxRequestViaForm = function (methodType,  reqData) {
        return new Promise((resolve, reject) => {
            //ShowLoader(); // Start the loader
            $.ajax({
                type: methodType,
                //url: UrlContent(),
                data: reqData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (response) {
                    //HideLoader(); // Hide the loader 
                    resolve(response);
                },
                error: function (xhr, status, error) {
                    //HideLoader(); // Hide the loader
                    reject({ xhr, status, error });
                }
            });
        });
    };
    this.ShowModal = function (modalId, content) {
        $("#" + modalId + "-content").html(content);
        $("#" + modalId).modal('show');
    }
    //this.ShowModal = function (modalId, content, formId) {
    //    $("#" + modalId + "-content").html(content);
    //    $("#" + modalId).modal('show');
    //}


    this.ShowModal = function (modalId, content, formId) {
        $("#" + modalId + "-content").html(content);
        $.validator.unobtrusive.parse($("#" + formId));
        $("#" + modalId).modal('show');
    }

    this.HideModal = function (modalId) {
        $("#" + modalId).modal('hide');
    }

    this.ToastrSuccess = function (message, gravity = 'right', position = 'top') {
        Toastify({
            text: message,
            duration: 3000,
            newWindow: true,
            close: true,
            gravity: position, // `top` or `bottom`
            position: gravity, // `left`, `center` or `right`
            stopOnFocus: true, // Prevents dismissing of toast on hover
            style: {
                background: "linear-gradient(to right, #00b09b, #96c93d)",
            },
            onClick: function () { } // Callback after click
        }).showToast();
    }



    this.ChangePassword = function () {
        ams.common.AjaxRequest('GET', 'Account/ChangePassword', {}).then((result) => {
            alert();
            ams.common.ShowModal("common-sm-modal", result, 'ChangePwdForm');
            alert(result);

        });
    }

    this.HandleLoadingButton = function (buttonSelector, callback) {
        var originalButton = $(buttonSelector).clone();
        // Replace the button content with the loading text and spinner
        $(buttonSelector).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...').prop('disabled', true);
        if (typeof callback === 'function') {
            callback(function () {
                // Revert to the original button content
                $(buttonSelector).replaceWith(originalButton);
            });
        }
    };

    this.InitMask = function () {
        if (document.querySelector(".mask-date")) {
            var cleaveDate = new Cleave('.mask-date', {
                date: true,
                delimiter: '-',
                datePattern: ['m', 'd', 'Y']
            });
        }


    }
    this.ToastrSuccess = function (msg, position, gravity) {
        Toastify({
            newWindow: true,
            text: msg,
            gravity: gravity,
            position: position,
            className: "bg-success",
            stopOnFocus: true,
            offset: {
                x: 50, 
            },
            duration: 3000,
            close: true,
            style: "style",
        }).showToast();
    }

    this.ToastrError = function (msg, position, gravity) {
        Toastify({
            newWindow: true,
            text: msg,
            gravity: gravity,
            position: position,
            className: "bg-danger",
            stopOnFocus: true,
            offset: {
                x: 50, 
            },
            duration: 3000,
            close: true,
            style: "style",
        }).showToast();
    }
}

