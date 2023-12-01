const botonesAgregar = document.querySelectorAll('.ver-detalle');

botonesAgregar.forEach(boton => {
    boton.addEventListener('click', agregarAlCarrito);
});

function agregarAlCarrito(event) {
    const boton = event.target;
    const producto = boton.closest('.product-item');

    const titulo = producto.querySelector('header h3').textContent;
    const precio = parseInt(producto.querySelector('header p:last-child').textContent.split(' ')[1]);

    agregarProductoAlCarrito(titulo, precio);
    actualizarTotal();
}

function agregarProductoAlCarrito(titulo, precio) {
    const itemCarrito = document.createElement('li');
    itemCarrito.innerHTML = `${titulo} - ${precio}$`;
    const listaProductos = document.querySelector('.lista-productos');
    listaProductos.appendChild(itemCarrito);
}

function actualizarTotal() {
    let total = 0;
    const precios = document.querySelectorAll('.lista-productos li');
    precios.forEach(precio => {
        total += parseInt(precio.textContent.split('$')[1]);
    });

    document.getElementById('total').textContent = total;
}