ams.auth = new function () {
    this.EncryptPwd = function () {
        let form = $("#loginform");
        if (form.valid()) {
            if ($("#txtUsername") != null) {
                //localStorage.removeItem('latestAnnouncements');
                localStorage.clear();
                var txtUserName = $("#txtUsername").val();
                var txtPassword = $("#password-input").val();
                //var txtCaptcha = $("#txtCaptcha").val();
                if (txtUserName == null || txtUserName == "" || typeof txtUserName == "undefined") {
                    return false;
                }
                else if (txtPassword == null || txtPassword == "" || typeof txtPassword == "undefined") {
                    return false;
                }
                //else if (txtCaptcha == null || txtCaptcha == "" || typeof txtCaptcha == "undefined") {
                //    return false;
                //}
                else {
                    ams.common.HandleLoadingButton("#signInBtnId", function (revert) {
                        ShowLoader();
                        var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                        var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
                        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtPassword), key,
                            {
                                keySize: 128 / 8,
                                iv: iv,
                                mode: CryptoJS.mode.CBC,
                                padding: CryptoJS.pad.Pkcs7
                            });
                        $("#password-input2").val(txtPassword);
                        // Continue with your logic, as needed
                        return true;
                    });
                }
            }
        }
    };

}
