window.onresize = () => {
    if (window.innerWidth < 576) {
        $("#signin").addClass('btn btn-sm btn-outline-dark');
    }
    else {
        $("#signin").removeClass('btn btn-sm btn-outline-dark');
    }

    if (window.innerWidth < 992) {
        $("#title").addClass('jumbotron');
    }
    else {
        $("#title").removeClass('jumbotron');
    }
}