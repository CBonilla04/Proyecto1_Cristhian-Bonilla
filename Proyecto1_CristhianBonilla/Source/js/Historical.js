function formatoFecha(fecha) {
    if (!fecha) {
        return '';
    }
    return new Date(fecha).toLocaleDateString('es-ES', {
        day: 'numeric',
        month: 'long',
        year: 'numeric',
        hour: 'numeric',
        minute: 'numeric'
    });
}

$(document).ready(function () {
    $('#scalesModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que activó el modal
        var flightIndex = button.data('flight-index');
        var reservationIndex = button.data('reservation-index');

        var reservation = JSON.parse($('#reservationData').text());
        var flight = reservation[reservationIndex].flights[flightIndex];
        var scales = flight.flightScales;
        var modal = $(this);
        var modalBody = modal.find('.scalesCard');
        modalBody.empty();

        if (scales.length > 0) {
            scales.forEach(function (scale) {
                modalBody.append('<p><strong>Vuelo:</strong> ' + scale.scales.number + '</p>');
                modalBody.append('<p><strong>Origen:</strong> ' + scale.scales.origin + '</p>');
                modalBody.append('<p><strong>Destino:</strong> ' + scale.scales.destination + '</p>');
                modalBody.append('<p><strong>Salida:</strong> ' + formatoFecha(scale.scales.departureDate) + '</p>');
                modalBody.append('<p><strong>Llegada:</strong> ' + formatoFecha(scale.scales.arriveDate) + '</p>');
                modalBody.append('<hr>');
            });
        } else {
            modalBody.append('<p>No hay escalas para este vuelo.</p>');
        }
    });
});

$(document).ready(function () {
    $('#passengersModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que activó el modal
        var reservationIndex = button.data('reservation-index');
        var reservation = JSON.parse($('#reservationData').text());
        var flightPassengers = reservation[reservationIndex].flightPassengers;
        var modal = $(this);
        var modalBody = modal.find('.passengersCard');
        modalBody.empty();

        if (flightPassengers.length > 0) {
            flightPassengers.forEach(function (passenger) {
                modalBody.append('<p><strong>Pasajero:</strong> ' + convertPassengerType(passenger.passengerType.type) + '</p>');
                modalBody.append('<p><strong>Tipo asiento:</strong> ' + passenger.passengerType.description + '</p>');
                modalBody.append('<p><strong>Precio Unitario:</strong> ' + '$'+passenger.unitPrice + '</p>');
                modalBody.append('<hr>');
            });
        } else {
            modalBody.append('<p>No hay pasajeros para este vuelo.</p>');
        }
    });
});