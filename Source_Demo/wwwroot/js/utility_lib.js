﻿const MAX_FILE_SIZE_IMAGE = 5;
const ACCEPT_EXTENSION_IMAGE_FILE = ".png,.jpg,.jpeg";
const MAX_IMAGE_FILE = 4;

//Declare icon html
var _loadAnimationSmallHtml = '<div class="spinner-border d-flex align-content-end flex-wrap" role="status" style="width:1rem;height:1rem;"><span class="sr-only">Loading...</span></div>',
    _iconEditHtml = '<i class="mdi mdi-square-edit-outline text-success"></i>',
    _iconAddHtml = '<i class="fa fa-plus-circle"></i>',
    _iconDeleteHtml = '<i class="mdi mdi-delete text-danger"></i>',
    _iconSearchHtml = '<i class="ti-icon"></i>',
    _iconPublishHtml = '<i class="mdi mdi-publish"></i>',
    _iconuPublishHtml = '<i class="mdi mdi-cancel"></i>',
    _iconViewHtml = '<i class="mdi mdi-eye"></i>',
    _iconGPSHtml = '<i class="icons8-google-maps"></i>';

//Declare loading overlay
var _overlaySuccessHtml = `<div class="overlay_update_success" style=" background-color: seagreen; display: flex; justify-content: center; align-items: center; opacity: 0.5; width: 91%; height: 100%; position: absolute; ">
                            <i class="mdi mdi-check mdi-36px text-white"></i>
                        </div>`;
var _loadingOverlay = '<div class="overlay-loading" style="display:none;"><div class="lds-ring"><div></div><div></div></div></div>';
var _loadingOverlay3Dot = '<div class="overlay-loading" style="display:none;"><div class="lds-ellipsis"><div></div><div></div><div></div></div>';
var _loadingButtonOverlay = `<div class="overlay-loading-button text-center" style="display:none;"><div class="lds-dual-ring"><div></div><div></div><div></div><div></div></div>`;


//Declare resources language in js
var _dataTableResource = {
    Search: "Tìm kiếm:",
    Show: "Xem",
    All: "Tất cả",
    Entries: "dòng",
    Showing: "Đang xem",
    To: "đến",
    Of: "của",
    Next: "<i class='mdi mdi-chevron-right'>",
    Previous: "<i class='mdi mdi-chevron-left'>",
    First: "Trang đầu",
    Last: "Trang cuối",
    EmptyTable: "Không có dữ liệu",
    LoadingRecords: "Đang tải...",
    ZeroRecords: "Không tìm thấy dữ liệu"
};
var _dataTablePaginationStyle = function () {
    $(".dataTables_paginate > .pagination").addClass("pagination-rounded");
};
var _buttonResource = {
    Add: "Thêm",
    AddNew: '<i class="fa fa-plus-circle"></i> Thêm mới',
    Edit: "Sửa",
    Update: "Cập nhật",
    Save: "Lưu",
    Pushlish: "Đăng",
    uPushlish: "Gỡ",
    Delete: "Xóa",
    Lock: "Khóa",
    UnLock: "Mở khóa",
    ChangePassword: "Đổi mật khẩu",
    ResetPassword: "Làm mới mật khẩu",
    ConfirmDelete: "Xác nhận xóa?",
    ConfirmLock: "Xác nhận khóa?",
    ConfirmUnLock: "Xác nhận mở khóa?",
    ConfirmRecover: "Xác nhận khôi phục?",
    View: "Xem",
    Accept: "Chấp nhận",
    Schedule: "Xếp lịch"
};
var _resultActionResource = {
    AddSuccess: "Thêm thành công",
    UpdateSuccess: "Cập nhật thành công",
    DeleteSuccess: "Đã xóa",
    LockSuccess: "Đã khóa",
    UnLockSuccess: "Đã mở khóa",
    RecoverSuccess: "Đã khôi phục",
    AddFail: "Thêm thất bại",
    UpdateFail: "Cập nhật thất bại",
    DeleteFail: "Xóa thất bại",
    ErrorAction: "Kết nối đến máy chủ không ổn định",
    PleaseWrite: "Bạn chưa nhập đủ hoặc thông tin không hợp lệ",
};

var _languageDataTalbeObj = {
    url: '/libs/datatables.net/js/i18n/vi.json',
};

//var _languageDataTalbeObj = {
//    emptyTable: _dataTableResource.EmptyTable,
//    //lengthMenu: '<div class="dataTables_length" id="main_table_length"><label>' + "Hiển thị" + ' <select name="main_table_length" aria-controls="main_table" class="custom-select custom-select-sm form-control form-control-sm"><option value="10">10</option><option value="15">15</option><option value="25">25</option><option value="50">50</option><option value="100">100</option><option value="-1">' + _dataTableResource.All + '</option></select> ' + _dataTableResource.Entries + '</label></div>',
//    search: _dataTableResource.Search,
//    paginate: {
//        first: _dataTableResource.First,
//        last: _dataTableResource.Last,
//        next: _dataTableResource.Next,
//        previous: _dataTableResource.Previous
//    },
//    loadingRecords: _dataTableResource.LoadingRecords,
//    zeroRecords: _dataTableResource.ZeroRecords,
//    info: _dataTableResource.Showing + " _START_ " + _dataTableResource.To + " _END_ " + _dataTableResource.Of + " _TOTAL_ " + _dataTableResource.Entries
//};

//Declare current page
var _currentPageUrl = location.pathname.split("/")[1] + "/" + location.pathname.split("/")[2];;

const _loadAnimationSpinnerHtml = function (isSpinner, text) {
    var spinner = `<button class="btn btn-sm" style="padding:0px 0px;" type="button" disabled=""><span class="spinner-border spinner-border-sm mr-1" role="status" aria-hidden="true"></span>${text}</button>`;
    if (isSpinner == 1)
        return spinner;
    return text;
}

//Check status code to redirect
function CheckResponseIsSuccessToRedirect(data) {
    if (data.error.code !== 200) {
        location.href = `/error/${data.error.code}`;
        return false;
    }
    return true;
}

//Check response is success
function CheckResponseIsSuccess(data) {
    var type = "warning";
    let isSuccess = false;
    switch (parseInt(data.result)) {
        case -1: type = "error"; ListErrorCodeAndMessage(data, type); break;
        case 0: type = "warning"; ShowToastNoti(type, '', data.error.message); break;
        case 1: type = "success"; isSuccess = true; break;
        default: break;
    }
    return isSuccess;
}

//List error code
function ListErrorCodeAndMessage(data, type) {
    switch (parseInt(data.error.code)) {
        case -2: ShowToastNoti(type, '', "Có gì đó không đúng!"); break;
        case -1: ShowToastNoti(type, '', "Có lỗi trong quá trình truy cập dữ liệu!"); break;
        case 400: ShowToastNoti(type, "Lỗi 400!", "Yêu cầu không hợp lệ!"); break;
        case 401:
            ShowToastNoti(type, "Lỗi 401!", "Chưa xác thực hoặc hết phiên. Vui lòng đăng nhập lại!"); SignInAgain();
            /*setTimeout(function () { location.href = '/account/signin?ReturnUrl=' + location.pathname; }, 2000);*/ break;
        case 403: ShowToastNoti(type, "Lỗi 403!", "Không có quyền truy cập!"); break;
        case 404: ShowToastNoti(type, "Lỗi 404!", "Không tìm thấy tài nguyên được yêu cầu!"); break;
        case 405: ShowToastNoti(type, "Lỗi 405!", "Phương thức yêu cầu vô hiệu hóa!"); break;
        case 406: ShowToastNoti(type, "Lỗi 406!", "Không tìm thấy bất kỳ nội dung nào với yêu cầu đưa ra!"); break;
        case 407: ShowToastNoti(type, "Lỗi 407!", "Yêu cầu xác thực proxy!"); break;
        case 408: ShowToastNoti(type, "Lỗi 408!", "Phiên đăng nhập hết hạn! Đang thử tạo lại phiên mới...", 3000); LogoutJs(); break;
        case 409: ShowToastNoti(type, "Lỗi 409!", "Yêu cầu xung đột với máy chủ!"); break;
        case 410: ShowToastNoti(type, "Lỗi 410!", "Nội dung yêu cầu đã bị xóa!"); break;
        case 411: ShowToastNoti(type, "Lỗi 411!", "Máy chủ từ chối yêu cầu!"); break;
        case 411: ShowToastNoti(type, "Lỗi 411!", "Máy chủ từ chối yêu cầu!"); break;
        case 412: ShowToastNoti(type, "Lỗi 412!", "Điều kiện tiên quyết không được đáp ứng!"); break;
        case 413: ShowToastNoti(type, "Lỗi 413!", "Dữ liệu yêu cầu vượt giới hạn!"); break;
        case 414: ShowToastNoti(type, "Lỗi 414!", "URI yêu cầu dài hơn mức quy định!"); break;
        case 415: ShowToastNoti(type, "Lỗi 415!", "Định dạng của dữ liệu được yêu cầu không được máy chủ hỗ trợ!"); break;
        case 416: ShowToastNoti(type, "Lỗi 416!", "Không thể thực hiện phạm vi yêu cầu!"); break;
        case 417: ShowToastNoti(type, "Lỗi 417!", "Máy chủ không thể đáp ứng được yêu cầu!"); break;
        case 421: ShowToastNoti(type, "Lỗi 421!", "Yêu cầu được hướng đến một máy chủ không thể tạo phản hồi!"); break;
        case 423: ShowToastNoti(type, "Lỗi 423!", "Tài nguyên đang được truy cập bị khóa!"); break;
        case 424: ShowToastNoti(type, "Lỗi 424!", "Yêu cầu không thành công do không thực hiện được yêu cầu trước đó!"); break;
        case 428: ShowToastNoti(type, "Lỗi 428!", "Máy chủ gốc yêu cầu yêu cầu phải có điều kiện!"); break;
        case 429: ShowToastNoti(type, "Lỗi 429!", "Bạn đang yêu cầu quá nhiều trong một khoảng thời gian nhất định!"); break;
        case 431: ShowToastNoti(type, "Lỗi 431!", "Máy chủ không thể xử lý yêu cầu vì tiêu đề quá lớn!"); break;
        case 451: ShowToastNoti(type, "Lỗi 451!", "Tài nguyên yêu cầu không thể được cung cấp một cách hợp pháp!"); break;
        case 500: /*ShowToastNoti(type, "Lỗi 401!", "Chưa xác thực hoặc hết phiên. Vui lòng đăng nhập lại!");*/
            ShowToastNoti(type, "Lỗi 500!", `Máy chủ không thể xử lý yêu cầu! ${!IsNullOrEmty(data.error.message) ? "<br/>" + data.error.message : ""}`); break;
        case 501: ShowToastNoti(type, "Lỗi 501!", "Phương thức yêu cầu không được máy chủ hỗ trợ và không thể xử lý được!"); break;
        case 502: ShowToastNoti(type, "Lỗi 502!", "Máy chủ nhận được một phản hồi không hợp lệ!"); break;
        case 503: ShowToastNoti(type, "Lỗi 503!", "Máy chủ xảy ra lỗi trong quá trình xử lý yêu cầu!"); break;
        case 504: ShowToastNoti(type, "Lỗi 504!", "Máy chủ không thể nhận được phản hồi kịp thời!"); break;
        case 505: ShowToastNoti(type, "Lỗi 505!", "Phiên bản HTTP được sử dụng trong yêu cầu không được máy chủ hỗ trợ!"); break;
        case 506: ShowToastNoti(type, "Lỗi 506!", "Máy chủ có lỗi cấu hình nội bộ!"); break;
        case 507: ShowToastNoti(type, "Lỗi 507!", "Máy chủ không đủ tài nguyên!"); break;
        case 508: ShowToastNoti(type, "Lỗi 508!", "Máy chủ đã phát hiện một vòng lặp vô hạn trong khi xử lý yêu cầu!"); break;
        case 510: ShowToastNoti(type, "Lỗi 510!", "Cần có các phần mở rộng khác cho yêu cầu để máy chủ đáp ứng yêu cầu đó!"); break;
        case 511: ShowToastNoti(type, "Lỗi 511!", "Máy khách cần xác thực để có quyền truy cập mạng!"); break;
        default: ShowToastNoti(type, '', data.error.message); break;
    }
}

//Clear validate form css
function RemoveClassValidate(elm) {
    try {
        $(elm).removeClass("was-validated");
        $(elm).find('input').removeClass("parsley-success");
        $(elm).find('input').removeClass("parsley-error");
        $('.parsley-errors-list').remove();
    } catch (e) {

    }
    //Remove other
    //$("#detailEditor").summernote("code", ""); //clear detail
    //myDropzone.removeAllFiles(); //clear file dropzone
    //$(".dropify-clear").click(); //clear dropify
}

//Show message error
function ShowToastNoti(type, title, message, timeout = 2000, position = 'topRight') {
    let icon = '<i class="iziToast-icon ico-question revealIn"></i>', color = '';
    switch (type) {
        case 'success': icon = '<i class="iziToast-icon ico-success revealIn"></i>'; color = 'green'; break;
        case 'warning': icon = '<i class="iziToast-icon ico-warning revealIn"></i>'; color = 'orange'; break;
        case 'error': icon = '<i class="iziToast-icon ico-error revealIn"></i>'; color = 'red'; break;
        case 'info': icon = '<i class="iziToast-icon ico-info revealIn"></i>'; color = 'blue'; break;
        case 'question': icon = '<i class="iziToast-icon ico-question revealIn"></i>'; color = 'yellow'; break;
        default: break;
    }
    iziToast.show({
        icon: icon,
        color: color,
        displayMode: 0,
        title: title,
        message: message,
        position: position,
        progressBar: true,
        timeout: timeout,
        transitionIn: 'flipInX',
        transitionOut: 'flipOutX',
        progressBarColor: 'rgb(0, 255, 184)',
        imageWidth: 70,
        layout: 2,
        iconColor: 'rgb(0, 255, 184)'
    });
}

//Show hide panel
function ShowHidePanel(panelShow, panelHide) {
    $(panelHide).slideUp(100);
    setTimeout(function () {
        $(panelShow).slideDown(200);
    }, 100);
}

//show hide pande fadeout in
function ShowHidePanelOI(panelShow, panelHide) {
    $(panelHide).fadeOut(100);
    setTimeout(function () {
        $(panelShow).fadeIn(100);
    }, 100);
}

//Show panel
function ShowPanel(panel) {
    $(panel).slideDown(200);
}

//Hide panel
function HidePanel(panel) {
    $(panel).fadeOut(200);
}

//Back to talbe
function BackToTable() {
    ShowHidePanelOI('#div_main_table', '#div_view_panel');
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    setTimeout(function () {
        $('#div_view_panel').html('');
    }, 200);
}

//Show overlay
function ShowOverlay(elm) {
    var $loading = $($(elm).children()[0]);
    if (!$loading.hasClass('overlay-loading')) {
        $(elm).prepend(_loadingOverlay);
        $($(elm).children()[0]).fadeIn(100);
    }
}

//Hide overlay
function HideOverlay(elm) {
    var $loading = $($(elm).children()[0]);
    if ($loading.hasClass('overlay-loading')) {
        $loading.fadeOut(100);
        setTimeout(function () {
            $loading.remove();
        }, 150);
    }
}

//Show overlay update success
function ShowOverlayUpdateSuccess(elm) {
    elm.prepend(_overlaySuccessHtml);
    $('.overlay_update_success').fadeOut(1000);
    setTimeout(function () {
        $('.overlay_update_success').remove();
    }, 1100);
}

//Show sweet alert
function ShowSweetAlert(type, title, message, url, id, messageSuccess) {
    swal.fire({
        title: title,
        text: message,
        type: type,
        showCancelButton: !0,
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        confirmButtonClass: "btn btn-success mt-2",
        cancelButtonClass: "btn btn-danger ml-2 mt-2",
        buttonsStyling: !1,
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: {
                        id: id
                    },
                    dataType: 'json',
                    success: function (response) {
                        //Check error code
                        if (response.data == null && response.code != null) {
                            CheckResponseIsSuccess(response);
                            resolve();
                            return;
                        }

                        switch (response.isSuccess) {
                            case true: swal.fire(messageSuccess, '', 'success'); break;
                            case false: ShowToastNoti('warning', '', response.messageErr); resolve(); break;
                            default: ShowToastNoti('warning', '', _resultActionResource.ErrorAction); resolve(); break;
                        }
                    },
                    error: function () {
                        ShowToastNoti('error', '', _resultActionResource.ErrorAction);
                        resolve();
                    }
                });
            });
        }
    });
}

//Add first row DataTalbe
function AddFirstRowDataTable(table, data) {
    //Add row data
    table.row.add(data).draw(false).node();

    //Set current page
    var currentPage = table.page();

    //move added row to desired index
    var rowCount = table.data().length - 1,
        insertedRow = table.row(rowCount).data(),
        tempRow;
    for (var i = rowCount; i >= 1; i--) {
        tempRow = table.row(i - 1).data();
        table.row(i).data(tempRow);
        table.row(i - 1).data(insertedRow);
    }

    //refresh the current page
    table.page(currentPage).draw(false);

    //Add animation new row
    var rowNode = table.row(0).node();
    RunAnimationAddRowNode(rowNode);
}

//Run animation add
function RunAnimationAddRowNode(rowNode) {
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    setTimeout(function () {
        //4a81d4
        $(rowNode).css({ "background-color": "#339933", "color": "white", "fontSize": ".1rem", "opacity": "0" })
            .animate({
                fontSize: '.75rem',
                opacity: '1'
            }, 300);
        setTimeout(function () {
            if ($(rowNode).hasClass('odd')) {
                $(rowNode).css({ "background-color": "#f3f7f9", "transition": "3s", "color": "#6c757d" });
            } else {
                $(rowNode).css({ "background-color": "#fff", "transition": "3s", "color": "#6c757d" });
            }
            setTimeout(() => {
                $(rowNode).removeAttr("style");
            }, 3000);
        }, 300);
    }, 800);
}

//Run animation delete
function RunAnimationDeleteRowNode(rowNode) {
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    $(rowNode).css({ "fontSize": ".875rem", opacity: "1" })
        .animate({
            fontSize: '.1rem',
            opacity: '0'
        }, 500);
}

//Run animation edit
function RunAnimationEditRowNode(rowNode) {
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    setTimeout(function () {
        //FFCC33 | bb000c
        $(rowNode).css({ "background-color": "#18c355", "color": "#FF0000", "font-weight": "bold" }).animate({
            opacity: '0.7'
        }, 300);
        $(rowNode).animate({
            opacity: '1'
        }, 300);
        $(rowNode).css({ "background-color": "#fff", "transition": "3s", "color": "#6c757d" });
        setTimeout(() => {
            $(rowNode).removeAttr("style");
        }, 2000);
    }, 200);
}

//Change UI Add
function ChangeUIAdd(table, data) {
    AddFirstRowDataTable(table, data);
}

//Change UI Edit
function ChangeUIEdit(table, id, data) {
    var rowIdx;
    var rowData = table.row(
        function (idx, data, node) {
            if (data.id == id) {
                rowIdx = idx;
                return idx.toString();
            }
        }).data();
    Object.keys(data).forEach((item, index) => rowData[item] = data[item]);
    table.row(rowIdx).data(rowData).draw(false);
    var rowNode = table.row(function (idx, data, node) { if (data.id == id) return idx.toString(); }).node();
    RunAnimationEditRowNode(rowNode);
}

//Change UI Delete
function ChangeUIDelete(table, id) {
    var rowNode = table.row(function (idx, data, node) { if (data.id == id) return idx.toString(); }).node();
    RunAnimationDeleteRowNode(rowNode);
    setTimeout(() => table.row(function (idx, data, node) { if (data.id == id) return idx.toString(); }).remove().draw(false), 500);
}

//Disabled key alphabet
function IsNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

//Init editor content summernote
function InitEditorContent(element, name, palaceHolder) {
    $(element).summernote({
        codeviewFilter: true,
        codeviewIframeFilter: true,
        placeholder: palaceHolder,
        height: 230,
        lang: "vi-VN",
        callbacks: {
            onInit: function (e) {
                $(e.editor).find(".custom-control-description").addClass("custom-control-label").parent().removeAttr("for")
            },
            onChange: function (contents, $editable) {
                $(name).val(contents);
            },
            onImageUpload: function (files) {

                for (let i = 0; i < files.length; i++) {
                    $.upload(files[i]);
                }
            }
        }
    });
    if ($(name).val() != '') {
        $(element).summernote('code', $(name).val());
    }
    $.upload = function (file) {
        let out = new FormData();
        out.append('imgFile', file, file.name);
        $.ajax({
            method: 'POST',
            url: '/Image/ImageUpload',
            contentType: false,
            processData: false,
            cache: false,
            data: out,
            success: function (response) {
                if (response.result == 1 && response.data != null)
                    $(element).summernote('insertImage', response.data.smallUrl);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ShowToastNoti('error', '', _resultActionResource.ErrorAction);
            }
        });
    };
    $.ajax({
        url: "https://api.github.com/emojis",
        async: !1
    }).then(function (e) {
        window.emojis = Object.keys(e), window.emojiUrls = e
    });
}

//Range value input number
function RangeValueInputNumber(element) {
    $(element).on('change keyup paste', function () {
        if (parseInt($(this).val()) > parseInt($(this).attr('max'))) {
            $(this).val($(this).attr('max'));
        }
        if (parseInt($(this).val()) < parseInt($(this).attr('min'))) {
            $(this).val($(this).attr('min'));
        }
    });
}

//Range numberic
function OnChangeRangeNumberic(elm) {
    let min = parseInt($(elm).attr('min'));
    let max = parseInt($(elm).attr('max'));
    if ($(elm).val() > max) {
        $(elm).val(max);
    } else if ($(elm).val() < min) {
        $(elm).val(min);
    }
}

//Range numberic
function OnChangeRangeNumbericWithMaxElm(elm, elmMax) {
    let min = parseInt($(elm).attr('min'));
    let max = parseInt($(elmMax).val());
    if (isNaN(max)) {
        $(elm).val(0);
    }
    if ($(elm).val() > max) {
        $(elm).val(max);
    } else if ($(elm).val() < min) {
        $(elm).val(min);
    }
}

//Range numberic
function OnChangeRangeNumbericWithMinElm(elm, elmMin) {
    let min = parseInt($(elmMin).val());
    let max = parseInt($(elm).attr('max'));
    if (isNaN(min)) {
        $(elm).val(0);
    }
    if ($(elm).val() > max) {
        $(elm).val(max);
    } else if ($(elm).val() < min) {
        $(elm).val(min);
    }
}

//clear content modal
function clearDiv(id) {
    $(id).on('hidden.bs.modal', function () {
        setTimeout(function () {
            $(id).empty();
        }, 100)
    })
}

//Check validateion in lib Unobtrusive js
function CheckValidationUnobtrusive(formElm) {
    $.validator.unobtrusive.parse(formElm);
    formElm.validate();
    //$.each(formElm.validate().errorList, function (key, value) {
    //    $errorSpan = formElm.find(`span[data-valmsg-for="${value.element.id}"]`);
    //    $errorSpan.html(`<span>${value.message}</span>`);
    //    $errorSpan.show();
    //});
    return formElm.valid();
}

function HtmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function SetCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function GetCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
        return "";
    }
}

function CopyValue(elm) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($(elm).val()).select();
    document.execCommand("copy");
    $temp.remove();
    ShowToastNoti('success', '', 'Đã sao chép!');
}

function CopyText(text) {
    var textField = document.createElement('textarea');
    textField.innerText = text;
    document.body.appendChild(textField);
    textField.select();
    textField.focus(); //SET FOCUS on the TEXTFIELD
    document.execCommand('copy');
    textField.remove();
    ShowToastNoti('success', '', 'Đã sao chép!');
}

function IsNullOrEmty(string) {
    return string == null || string === "";
}

function SignOut(elm) {
    $(elm).attr('disabled', true);
    $('#confirmLogout').modal('show');
    setTimeout(function () {
        $(elm).attr('disabled', false);
    }, 500);
}

function SignInAgain() {
    $('#confirmLoginAgain').modal('show');
}

function RemoveElementInArray(array, elem) {
    var index = array.indexOf(elem);
    if (index > -1) {
        array.splice(index, 1);
    }
    return array;
}

function LogoutJs() {
    ShowToastNoti("warning", "", "408! Session timeout...");
    setTimeout(function () { location.href = '/Account/SignOut?ReturnUrl=' + location.pathname; }, 2000);
}

function FormatToVND(value) {
    if (IsNullOrEmty(value))
        return "NaN";
    return value.toLocaleString('vi', { style: 'currency', currency: 'VND' }).replace(/\s₫/g, ' đ');
}

function PrintElem(elem) {
    //var mywindow = window.open('', 'PRINT', 'height=400,width=600');
    mywindow = window.open("");

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById(elem).innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}

function PrintHtml(html) {
    //var mywindow = window.open('', 'PRINT', 'height=400,width=600');
    mywindow = window.open("");

    mywindow.document.write(html);

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    setTimeout(function () {
        mywindow.print();
        mywindow.close();
    }, 800);

    return true;
}

function PrintBase64PDF(base64, title) {
    let pdfWindow = window.open("");
    pdfWindow.document.write('<html><head><title>' + title + '</title>');
    pdfWindow.document.write('</head><body >');
    pdfWindow.document.write("<iframe width='100%' height='100%' src='data:application/pdf;base64, " + encodeURI(base64) + "'></iframe>");
    pdfWindow.document.write('</body></html>');
}

function encodeImageFileAsURL(element, viewDataElement) {
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        $(viewDataElement).val(reader.result);
    }
    reader.readAsDataURL(file);
}

function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

function FormatViewCurrency(elm) { // <input type="text" name="money" onkeypress="return IsNumberKey(event)" onpaste="event.preventDefault()" onkeyup="FormatViewCurrency(this)" />
    let value = $(elm).val();
    let breakS = ',';
    if (IsNullOrEmty(value)) return false;
    let subStringVal = parseInt(value.toString().replace(/,/g, "")).toString();
    let array = subStringVal.split('');
    let result = '';
    let dem = -1;
    for (var i = array.length - 1; i >= 0; i--) {
        dem++;
        if (dem % 3 === 0 && result.length > 2)
            result = array[i] + breakS + result;
        else
            result = array[i] + result;
    }
    dem = -1;
    $(elm).val(result);
}
function ClearFormatViewCurrencyToNumber(value) {
    return !IsNullOrEmty(value) ? parseFloat(value.toString().replace(/,/g, "").replace(/./g, "")) : 0;
}

function AddDaysInput(id, currentDate, addDays, format) {
    var date = new Date(currentDate);
    var day = date.addDays(addDays).getDate();// (date.getDate()+addDays);// + addDays;
    var month = date.addDays(addDays).getMonth() + 1;// date.getMonth() + 1;
    var year = date.addDays(addDays).getFullYear();
    var hour = date.addDays(addDays).getHours();
    var minutes = date.addDays(addDays).getMinutes();
    var seconds = date.addDays(addDays).getSeconds();

    if (month < 10)
        month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = '';
    if (format == 0)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 1)
        today = year + "-" + month + "-" + day;//yyyy-MM-dd
    if (format == 2)
        return finalDate = day + " " + month + "-" + year + "-" + hour + ":" + minutes;// dd MM-yyyy-HH:mm
    if (format == 3)
        today = day + "-" + month + "-" + year;//dd-MM-yyyy
    if (format == 4)
        today = year + "/" + month + "/" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 5)
        today = year + "/" + month + "/" + day;//yyyy/MM/dd 
    if (format == 6)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;//dd/MM/yyyy HH:mm
    if (format == 7)
        today = day + "/" + month + "/" + year;
    if (format == 8)
        today = month + "/" + day + "/" + year + " " + hour + ":" + minutes;//mm/dd/yyyy HH:mm
    if (format == 9)
        today = month + "/" + day + "/" + year;
    if (format == 10)
        today = year + "" + month + "" + day + "_" + hour + "" + minutes;//yyyyMMdd_HHmm
    if (format == 11)
        today = day + "/" + month + "/" + year;// + " " + hour + ":" + minutes;
    if (format == 12)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;
    if (format == 13)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes + ":" + seconds;//dd/mm/yyyy HH:mm:ss
    if (format == 14)
        today = day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds;//dd-mm-yyy HH:mm:ss
    if (format == 15)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + seconds; //yyyy-mm- dd HH:mm:ss
    //var today = day + "/" + month + "/" + year;
    $(id).val(today);
}

function AddDaysDiv(id, currentDate, addDays, format) {
    var date = new Date(currentDate);
    var day = date.addDays(addDays).getDate();// (date.getDate()+addDays);// + addDays;
    var month = date.addDays(addDays).getMonth() + 1;// date.getMonth() + 1;
    var year = date.addDays(addDays).getFullYear();
    var hour = date.addDays(addDays).getHours();
    var minutes = date.addDays(addDays).getMinutes();
    var seconds = date.addDays(addDays).getSeconds();

    if (month < 10)
        month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = '';
    if (format == 0)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 1)
        today = year + "-" + month + "-" + day;//yyyy-MM-dd
    if (format == 2)
        return finalDate = day + " " + month + "-" + year + "-" + hour + ":" + minutes;// dd MM-yyyy-HH:mm
    if (format == 3)
        today = day + "-" + month + "-" + year;//dd-MM-yyyy
    if (format == 4)
        today = year + "/" + month + "/" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 5)
        today = year + "/" + month + "/" + day;//yyyy/MM/dd 
    if (format == 6)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;//dd/MM/yyyy HH:mm
    if (format == 7)
        today = day + "/" + month + "/" + year;
    if (format == 8)
        today = month + "/" + day + "/" + year + " " + hour + ":" + minutes;//mm/dd/yyyy HH:mm
    if (format == 9)
        today = month + "/" + day + "/" + year;
    if (format == 10)
        today = year + "" + month + "" + day + "_" + hour + "" + minutes;//yyyyMMdd_HHmm
    if (format == 11)
        today = day + "/" + month + "/" + year;// + " " + hour + ":" + minutes;
    if (format == 12)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;
    if (format == 13)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes + ":" + seconds;//dd/mm/yyyy HH:mm:ss
    if (format == 14)
        today = day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds;//dd-mm-yyy HH:mm:ss
    if (format == 15)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + seconds; //yyyy-mm- dd HH:mm:ss
    //var today = day + "/" + month + "/" + year;
    $(id).text(today);
}
