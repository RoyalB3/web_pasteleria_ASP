$(document).ready(function(){
    $('.category-list .category-item[category="all"]').addClass('ct-item-active');
    
    $('.category-item').click(function(){
        
        let filterProd = $(this).attr('category');
        
        $('.category-item').removeClass('ct-item-active');
        $(this).addClass('ct-item-active');
        $('.product-item').hide();
        $('.product-item[category="'+filterProd+'"]').show();

    })
    $('.category-item[category="all"]').click(function(){
        $('.product-item').show();
    })
});
