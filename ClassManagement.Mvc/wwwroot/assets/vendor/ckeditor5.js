import {
    ClassicEditor,
    Heading,
    Essentials,
    Paragraph,
    Bold,
    Italic,
    Font,
    Link,
    List
} from 'ckeditor5';

let editorInstance = null;

function attachCKEditor() {
    if (editorInstance) {
        editorInstance.destroy();
        editorInstance = null;
    }

 ClassicEditor
        .create(document.querySelector('#ck-content'), {
            plugins: [Essentials, Paragraph, Bold, Italic, Font, Heading, Link, List],
            toolbar: ['undo', 'redo', '|', 'heading', '|', 'bold', 'italic', 'link', '|', 'bulletedList', 'numberedList']
        })
        .then(editor => {
            editor.model.document.on('change', () => {
                $("#ck-content").html(editor.getData());
            });
            editorInstance = editor;
        })
        .catch(err => {
            console.error(err.stack);
        });
};

$(document).on('ready', function () {
    if (!editorInstance)
    attachCKEditor();
});

$(document).ajaxComplete(function () {
    attachCKEditor();
});