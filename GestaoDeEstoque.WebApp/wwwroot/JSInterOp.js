function PreventFormSubmition(formId) {
    document.getElementById(`${formId}`).addEventListener("keydown", function (event) {

        if (event.keyCode == 13) { //quando teclar o enter
            event.preventDefault();
            return false;
        }
    })
}