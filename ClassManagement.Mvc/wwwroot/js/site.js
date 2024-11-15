$(function () {
    $("#loaderbody").addClass('hide');

    $(document).on('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    });
    $(document).on('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

//Hiển thị modal
showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal({
                backdrop: 'static',
                keyboard: false
            });
            $('#form-modal').modal('show');
        }
    })
}

//Post
jQueryPost = (elementId, form, message) => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    if (res.isReload) {
                        $('#form-modal .modal-body').html('');
                        $('#form-modal .modal-title').html('');
                        $('#form-modal').modal('hide');
                        $.notify(message + ' đã lưu', { globalPosition: 'top right', className: 'success', autoHideDelay: 2000 });
                        setTimeout(function () {
                            window.location.reload();
                        }, 1000);
                    } else {
                        $(elementId).html(res.html)
                        $('#form-modal .modal-body').html('');
                        $('#form-modal .modal-title').html('');
                        $('#form-modal').modal('hide');
                        $.notify(message + ' đã lưu', { globalPosition: 'top right', className: 'success', autoHideDelay: 2000 });
                    }
                }
                else {
                    $('#form-modal .modal-body').html(res.html);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

//Create
jQueryOnCreate = (form, message, viewName) => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $.notify(message + ' đã được tạo', { globalPosition: 'top right', className: 'success', autoHideDelay: 2000 });
                    $(form).find('input, button').prop('disabled', true);
                    $(form).find('input[type="submit"]').val("Đang xử lý...");
                    setTimeout(function () {
                        window.location.href = viewName;
                    }, 2000);
                } else {
                    $(form).html(res.html);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

//Thêm học sinh vào lớp
jQueryOnAdd = (form, viewName) => {
    var studentsId = [];
    $('input[id="selectedStudents"]:checked').each(function () {
        studentsId.push($(this).val());
    });
    var id = window.location.pathname.split('/')[4];
    try {
        var formData = new FormData($(form)[0]);
        formData.append('id', id);
        formData.append('model', studentsId);
        $.ajax({
            type: 'POST',
            url: form.action,
            contentType: false,
            processData: false,
            data: formData,
            traditional: true,
            success: function (res) {
                if (res.isValid) {
                    $.notify('Thêm thành công', { globalPosition: 'top right', className: 'success', autoHideDelay: 2000 });
                    setTimeout(function () {
                        window.location.href = window.location.origin + '/Admin/Class/' + viewName + '/' + id;
                    }, 2000);
                } else {
                    $(form).html(res.html);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
};

//Filter và Sort danh sách
document.addEventListener('DOMContentLoaded', function () {
    var selects = document.querySelectorAll('.datatable-selector');
    var items = [], str = '';
    selects.forEach(function (key) {
        var item = key.getAttribute('name');
        items.push(item);
    });
    selects.forEach(function (select) {
        select.addEventListener('change', function () {

            var action = window.location.pathname;

            var url = action + '?';
            Object.keys(items).forEach(function (item) {
                var value = document.querySelector('select[name="' + items[item] + '"]').value ?? null;

                if (value != null) str += items[item] + '=' + value + '&';
            });

            url += str;

            url = url.slice(0, -1);

            window.location.href = url;
        });
    });
});

//Hiển thị input ClassSize nếu ClassType != "Practice"
function toggleClassTypeInput() {
    var classType = document.getElementById("ClassType").value;
    var classSizeInput = document.getElementById("ClassSizeInput");

    if (classType === "Practice") {
        classSizeInput.style.display = "none";
    } else {
        classSizeInput.style.display = "block";
    }
}

//Hiển thị password
function showPasswordOrNot() {
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

//Xóa item
$(function () {
    $(document).on('submit', '.change-status', function (e) {
        e.preventDefault();
        var form = $(this);
        var confirmMsg = form.data('confirm-message');
        var currentUrl = window.location.pathname;
        if (confirm(confirmMsg)) {
            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (res) {
                    var classId = res.msg == 'Xóa' ? '#get-notifies-by-user' : '#get-notifies-deleted-by-user';
                    if (res.isValid) {
                        $(classId).html(res.html);
                        $('.datatable-pagination').load(currentUrl + " .datatable-pagination > *");
                        $.notify(`${res.msg} thành công`, { globalPosition: 'top center', className: 'success', autoHideDelay: 2000 });
                    } else {
                        $.notify(`${res.msg} không thành công`, { globalPosition: 'top center', className: 'error', autoHideDelay: 2000 });
                    }
                },
                error: function (e) {
                    alert('Đã xảy ra lỗi khi xóa thông báo');
                }
            });
        }
    });
});

//Xử lí countdown
function startCountdown(elementId, redirectUrl, countdownStart = 5) {
    let countdownTime = countdownStart;
    const countdownElement = document.getElementById(elementId);
    if (!countdownElement) return;

    const interval = setInterval(() => {
        countdownElement.textContent = countdownTime;
        countdownTime--;

        if (countdownTime < 0) {
            clearInterval(interval);
            window.location.href = redirectUrl;
        }
    }, 1000);
};
