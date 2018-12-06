// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const notificar = (tipo, msg) => {
    if (msg) {
        switch (tipo) {
            case 'sucesso':
                tipo = 'success';
                break;
            case 'erro':
                tipo = 'danger';
                break;
            case 'aviso':
                tipo = 'warning';
                break;
            default:
                tipo = 'info';
        }
        var $alert = $(`<div class="alert alert-${tipo} alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>${msg}</div>`).hide();
        $('body').prepend($alert);
        $alert.fadeIn('short');
        setTimeout(() => $alert.fadeOut('short', () => $alert.remove()), 5000);
    }
};

$(document).ready(() => {
    $.extend($.fn.dataTable.defaults, {
        bSearching: false,
        bSort: false,
        bServerSide: true,
        sAjaxDataProp: 'data' 
    });

    $.ajax({
        url: '/js/datatables-pt-br.json',
        success: (data) => $.extend(
            $.fn.dataTable.defaults,
            { oLanguage: data }
        )
    });
});