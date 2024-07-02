$.validator.addMethod("maxFileSize",
    function (value, element, params) {
        if (element.files[0].size > 3 * 1024 * 1024) {
            return false;
        } else {
            return true;
        }
    }
);
$.validator.unobtrusive.adapters.addBool("maxFileSize");

$.validator.addMethod("fileExtentionLimit",
    function (value, element, params) {
        var extentions = ["jpeg", "jpg", "png"]
        var fileExtention = value.substr((value.lastIndexOf('.') + 1));
        for (let i = 0; i < extentions.length; i++) {
            if (fileExtention == extentions[i]) {
                return true;
            }
        }
        return false;
    }
);
$.validator.unobtrusive.adapters.addBool("fileExtentionLimit");