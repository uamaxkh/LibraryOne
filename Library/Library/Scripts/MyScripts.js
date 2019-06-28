function imgError(image) {
    image.onerror = "";
    image.src = "/TitlePic/noImage.png";
    return true;
}

function addFormSubmitConfirmation(formId, confirmationMessage) {
    $("#" + formId).on('submit',
        () => {
            var reallyDelete = confirm(confirmationMessage);
            if (reallyDelete) {
                return true;
            }
            return false;
        }
    );
}

