function imgError(image) {
    image.onerror = "";
    image.src = "/TitlePic/noImage.png";
    return true;
}

//<img onerror="imgError(this)" id="img" src="3.jpg">