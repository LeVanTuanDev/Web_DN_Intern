//$(document).ready(function () {
//    Login();
//});
//function Login() {
//    $("#form_login").on("submit", function (e) {
//        e.preventDefault();
//        e.stopImmediatePropagation();
//        let $formElm = $('#form_login');
//        let formData = new FormData($formElm[0]);
//        laddaSubmitForm = Ladda.create(document.querySelector('#btn_submit_login'));
//        laddaSubmitForm.start();
//        $.ajax({
//            url: '/Account/P_Login',
//            type: 'POST',
//            data: formData,
//            dataType: 'json',
//            contentType: false,
//            processData: false,
//            success: function (response) {
//                laddaSubmitForm.stop();
//                if (!CheckResponseIsSuccess(response)) {
//                    return false;
//                }
//                ShowToastNoti('success', '', _resultActionResource.LoginSuccess);
//                setTimeout(() => {
//                    window.location.href = '/trang-chu';
//                }, 500);
//            },
//            error: function (err) {
//                laddaSubmitForm.stop();
//                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
//            }
//        });
//    });
//}

//function Register() {
//    $("#form_register").on("submit", function (e) {
//        e.preventDefault();
//        e.stopImmediatePropagation();
//        let $formElm = $('#form_register');
//        let formData = new FormData($formElm[0]);
//        laddaSubmitForm = Ladda.create(document.querySelector('#btn_submit_register'));
//        laddaSubmitForm.start();
//        $.ajax({
//            url: '/Account/P_Register',
//            type: 'POST',
//            data: formData,
//            dataType: 'json',
//            contentType: false,
//            processData: false,
//            success: function (response) {
//                laddaSubmitForm.stop();
//                if (!CheckResponseIsSuccess(response)) {
//                    return false;
//                }
//                ShowToastNoti('success', '', _resultActionResource.RegisterSuccess);
//                setTimeout(() => {
//                    window.location.href = '/dang-nhap';
//                }, 500);
//            },
//            error: function (err) {
//                laddaSubmitForm.stop();
//                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
//            }
//        });
//    });
//}

//function Logout() {
//    Swal.fire({
//        title: "Đăng xuất",
//        text: "Bạn có chắc chắn muốn đăng xuất?",
//        icon: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#3085d6",
//        cancelButtonColor: "#d33",
//        cancelButtonText: "Hủy",
//        confirmButtonText: "Đăng xuất"
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: '/Account/P_Logout',
//                type: 'GET',
//                success: function (response) {
//                    if (!CheckResponseIsSuccess(response)) {
//                        return;
//                    }
//                    ShowToastNoti('success', '', _resultActionResource.LogoutSuccess);
//                    setTimeout(() => {
//                        window.location.href = '/dang-nhap';
//                    }, 500);
//                },
//                error: function (err) {
//                    ShowToastNoti('error', '', "Đăng xuất thất bại!");
//                }
//            });
//        }
//    });
//}

function Logout() {
    Swal.fire({
        title: "Đăng xuất",
        text: "Bạn có chắc chắn muốn đăng xuất",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        cancelButtonText: "Quay lại",
        confirmButtonText: "Đăng xuất"
    }).then((result) => {
        if (result.value) {
            $.ajax({

                url: '/Account/P_Logout',
                type: 'GET',
                contentType: false,
                processData: false,
                success: function (response) {
                    if (!CheckResponseIsSuccess(response)) {
                        return false;
                    }
                    ShowToastNoti('success', '', _resultActionResource.LogoutSuccess);

                    setTimeout(() => {
                        window.location.href = '/dang-nhap'
                    }, 500)
                }, error: function (err) {
                    CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
                }
            });
        }
    });
}

function Register() {
    $("#form_register").on("submit", function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        let $formElm = $('#form_register');
        let isvalidate = CheckValidationUnobtrusive($formElm);
        if (!isvalidate) { ShowToastNoti('warning', '', _resultActionResource.PleaseWrite); return false; }

        let formData = new FormData($formElm[0]);
        laddaSubmitForm = Ladda.create(document.querySelector('#btn_submit_register'));
        laddaSubmitForm.start();
        console.log(formData)
        $.ajax({
            url: '/Account/P_Register',
            type: 'POST',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                laddaSubmitForm.stop();
                if (!CheckResponseIsSuccess(response)) {
                    return false;
                }
                ShowToastNoti('success', '', _resultActionResource.RegisterSuccess);

                setTimeout(() => {
                    window.location.href = '/dang-nhap'
                }, 500)
            }, error: function (err) {
                laddaSubmitForm.stop();
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        });
    })

}

function Login() {
    $("#form_login").on("submit", function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        let $formElm = $('#form_login');
        let isvalidate = $formElm[0].checkValidity();
        if (!isvalidate) { ShowToastNoti('warning', '', _resultActionResource.PleaseWrite); return false; }
        let formData = new FormData($formElm[0]);
        laddaSubmitForm = Ladda.create(document.querySelector('#btn_submit_login'));
        laddaSubmitForm.start();
        console.log(formData)
        $.ajax({
            url: '/Account/P_Login',
            type: 'POST',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                console.log(response);
                laddaSubmitForm.stop();
                if (!CheckResponseIsSuccess(response)) {
                    return false;
                }
                ShowToastNoti('success', '', _resultActionResource.LoginSuccess);

                setTimeout(() => {
                    window.location.href = '/trang-chu';
                }, 500)
            }, error: function (err) {
                laddaSubmitForm.stop();
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        });
    })
}