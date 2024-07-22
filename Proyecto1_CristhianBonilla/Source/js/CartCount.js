function updateCartItemCount() {
    $.get('/Home/GetCartItemCount', function (data) {
        $('#cartItemCount').text(data.count);
    });
}
