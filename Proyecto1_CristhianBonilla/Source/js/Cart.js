function convertPassengerType(type) {
    switch (type) {
        case 'ADULT': return 'Adulto';
        case 'CHILD': return 'Niño';
        case 'HELD_INFANT': return 'Bebé';
        default: return 'Desconocido';
    }
}
$(document).on('hidden.bs.modal', '#detalleVueloModal', function (e) {
    // Asegurarse de que la pantalla principal esté habilitada después de cerrar el modal
    if ($('.modal.show').length > 0) {
        $('body').addClass('modal-open');
    }
});

$(document).on('hidden.bs.modal', '#detalleVueloModal', function (e) {
    // Asegurarse de que la pantalla principal esté habilitada después de cerrar el modal
    if ($('.modal.show').length > 0) {
        $('body').addClass('modal-open');
    }
});

$(document).on('hidden.bs.modal', function (e) {
    // Asegurarse de que la pantalla principal esté habilitada después de cerrar el modal
    if ($('.modal.show').length > 0) {
        $('body').addClass('modal-open');
    } else {
        $('.modal-backdrop').remove(); // Eliminar manualmente el backdrop
    }
});