function validarRut(rut) {
    function validarRut(rut) {
        condicion = false
        cantidad = 0

        while (rut[cantidad] != null) {
            cantidad = cantidad + 1
        }

        if (cantidad == 9) {

            rut = "0" + rut

        }

        if (rut[8] != "-") {
            return condicion
        }

        d1 = parseInt(rut[0]) * 3
        d2 = parseInt(rut[1]) * 2
        d3 = parseInt(rut[2]) * 7
        d4 = parseInt(rut[3]) * 6
        d5 = parseInt(rut[4]) * 5
        d6 = parseInt(rut[5]) * 4
        d7 = parseInt(rut[6]) * 3
        d8 = parseInt(rut[7]) * 2

        suma = d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8

        division = suma / 11

        decimales = division - parseInt(division)

        digito = 11 - (11 * decimales)

        digito = Math.round(digito)

        if (digito == 11) {
            digito = 0
        } else if (digito == 10) {
            digito = "k"
        }

        if (digito == rut[9]) {

            condicion = true
            return condicion
        } else {
            return condicion
        }
    }
}

function validarFormulario() {
    var rutInput = document.querySelector('input[name="rut"]');
    var rut = rutInput.value.trim();

    if (!validarRut(rut)) {
        alert('El Rut ingresado no es válido');
        rutInput.focus();
        return false;
    } else {
        alert('Rut validado')
        return true;
    }

     // Devuelve true para enviar el formulario si el Rut es válido
}