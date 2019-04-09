/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
        config.language = 'vi';
    config.filebrowserBrowseUrl = '/scripts/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/scripts/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/scripts/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Content/Admin/img-product';
    config.filebrowserFlashUploadUrl = '/scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/scripts/ckfinder/');
};
