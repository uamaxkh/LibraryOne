function imgError(image) {
    image.onerror = "";
    image.src = "noImage.png";
    return true;
}

//<img onerror="imgError(this)" id="img" src="3.jpg">