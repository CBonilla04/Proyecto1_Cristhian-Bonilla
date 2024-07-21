
// Convertir la duración de formato ISO 8601 a HH:MM
function convertIso8601DurationToTime(duration) {
    const match = duration.match(/PT(\d+H)?(\d+M)?/);
    const hours = match[1] ? match[1].slice(0, -1).padStart(2, '0') : '00';
    const minutes = match[2] ? match[2].slice(0, -1).padStart(2, '0') : '00';
    return `${hours}:${minutes}`;
}

function convertPassengerType(passenger) {
    let passengerType = '';
    switch (passenger) {
        case 'ADULT':
            passengerType = 'Adulto';
            break;
        case 'CHILD':
            passengerType = 'Niño';
            break;
        case 'HELD_INFANT':
            passengerType = 'Bebé';
            break;
        default:
            passengerType = 'Desconocido';
            break;
    }
    return passengerType;
}


function formatoFecha(fecha) {
    if (!fecha) {
        return '';
    }
    return new Date(fecha).toLocaleString('es-ES', { day: 'numeric', month: 'long', year: 'numeric', hour: 'numeric', minute: 'numeric' });
}

function SumarPrecios(ofertas)
{
    let total = 0;
    foreach(oferta in ofertas)
    {
        total += decimal.Parse(oferta?.Price?.Total);
    }
    return total;
}