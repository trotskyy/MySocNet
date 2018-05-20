(function() {
    var imageCoverDiv = document.getElementById('imageCover');
    var cancelButton = document.getElementById('loadUserImageDialogCancel');
    var okButton = document.getElementById('loadUserImageDialogOk');
    var loadUserImageDialog = document.getElementById('loadUserImageDialog');

    imageCoverDiv.addEventListener('click', function() {
        loadUserImageDialog.showModal();
    });

    cancelButton.addEventListener('click', function() {
        loadUserImageDialog.close();
    });
    okButton.addEventListener('click', function() {
        loadUserImageDialog.close();
    });

})();