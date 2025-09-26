var ams = ams || {};

ams.common = new function () {




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

