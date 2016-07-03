    /*

    Copyright (c) 2016 ztn
    */

    //this is the image displayed when there is no support for canvas
    var defaultImageSrc = "~/Content/Images/charRendered.png";
    //this is the default scale to render the image
    var scale = 1;
    var CanvasHeight = 0;
    var CanvasWidth = 0;

    function renderMCSkins(classNameIn, scaleIn, replacementImageIn) {
        scale = scaleIn || scale;
        defaultImageSrc = replacementImageIn || defaultImageSrc;
        //we need custom support for IE, because it doesn't support getElementsByClassName
        if (navigator.appName == "Microsoft Internet Explorer") {
            var skinImages = getElementsByClassName(classNameIn, 'img');
        } else {
            var skinImages = document.getElementsByClassName(classNameIn);
            //   CanvasWidth = $("." + classNameIn).width();
            //   CanvasHeight = $("." + classNameIn).height();
            //   console.log(CanvasHeight);
            //   console.log(CanvasWidth);
        }

        var canvasSupported = supportsCanvas();

        var skin = skinImages[0];
        //if canvas is supported, we render the image to a skin
        if (canvasSupported) {
            if (skin.complete) {
                renderSkin(skin);
            }
            else {
                skin.onload = handleSkinLoaded;
                skin.onerror = handleImageError;
            }
        } else {
            //if it's not supported, we use the default image
            skin.src = defaultImageSrc;
        }
    }

    function handleSkinLoaded() {
        renderSkin(this);
    }

    function handleImageError() {
        //create replacement image
        var replacement = new Image();
        replacement.src = defaultImageSrc;
        //we assign the same classname the image has, for CSS purposes
        replacement.setAttribute('class', this.getAttribute('class'));
        this.parentNode.replaceChild(replacement, this);
    }

    function renderSkin(skinImage) {
        var classname = skinImage.getAttribute('class');
        var c = 1;
        c = skinImage.width / 64;
        var canvas = document.createElement('canvas');
        canvas.width = 16 * scale;
        canvas.height = 32 * scale;

        if (/avaSkin/.test(classname)) {
            canvas.width = 15 * scale;
            canvas.height = 15 * scale;
        }

        //we assign the same classname the image has, for CSS purposes
        canvas.setAttribute('class', classname);
        var context = canvas.getContext("2d");
        var s = scale;
        context.mozImageSmoothingEnabled = false;
        context.msImageSmoothingEnabled = false;
        context.imageSmoothingEnabled = false;
        if (/frontSkin/.test(classname)) {
            //startFront
            context.scale(-1, 1);
            //draw the left leg
            context.drawImage(skinImage, 4 * c, 20 * c, 4 * c, 12 * c, -8 * s, 20 * s, -4 * s, 12 * s);
            //draw the left arm
            context.drawImage(skinImage, 44 * c, 20 * c, 4 * c, 12 * c, -12 * s, 8 * s, -4 * s, 12 * s);
            context.scale(-1, 1);

            //draw the head
            context.drawImage(skinImage, 8 * c, 8 * c, 8 * c, 8 * c, 4 * s, 0 * s, 8 * s, 8 * s);
            //draw the mask
            context.drawImage(skinImage, 40 * c, 8 * c, 8 * c, 8 * c, 4 * s, 0 * s, 8 * s, 8 * s);
            //draw the body
            context.drawImage(skinImage, 20 * c, 20 * c, 8 * c, 12 * c, 4 * s, 8 * s, 8 * s, 12 * s);
            //draw the right leg
            context.drawImage(skinImage, 4 * c, 20 * c, 4 * c, 12 * c, 4 * s, 20 * s, 4 * s, 12 * s);
            //draw the right arm
            context.drawImage(skinImage, 44 * c, 20 * c, 4 * c, 12 * c, 0 * s, 8 * s, 4 * s, 12 * s);

            if (skinImage.height / skinImage.width === 1) {
                //draw down left leg
                context.drawImage(skinImage, 20 * c, 52 * c, 4 * c, 12 * c, 4 * s, 20 * s, 4 * s, 12 * s);
                //draw down left arm
                context.drawImage(skinImage, 36 * c, 52 * c, 4 * c, 12 * c, 0 * s, 8 * s, 4 * s, 12 * s);
            }

            //endFront
        } else if (/backSkin/.test(classname)) {
            //startBack
            context.scale(-1, 1);
            //draw the right leg
            context.drawImage(skinImage, 12 * c, 20 * c, 4 * c, 12 * c, -4 * s, 20 * s, -4 * s, 12 * s);
            //draw the right arm
            context.drawImage(skinImage, 52 * c, 20 * c, 4 * c, 12 * c, -0 * s, 8 * s, -4 * s, 12 * s);
            context.scale(-1, 1);

            //draw the head
            context.drawImage(skinImage, 24 * c, 8 * c, 8 * c, 8 * c, 4 * s, 0 * s, 8 * s, 8 * s);
            //draw the mask
            context.drawImage(skinImage, 56 * c, 8 * c, 8 * c, 8 * c, 4 * s, 0 * s, 8 * s, 8 * s);
            //draw the body
            context.drawImage(skinImage, 32 * c, 20 * c, 8 * c, 12 * c, 4 * s, 8 * s, 8 * s, 12 * s);
            //draw the left leg
            context.drawImage(skinImage, 12 * c, 20 * c, 4 * c, 12 * c, 8 * s, 20 * s, 4 * s, 12 * s);
            //draw the left arm
            context.drawImage(skinImage, 52 * c, 20 * c, 4 * c, 12 * c, 12 * s, 8 * s, 4 * s, 12 * s);

            if (skinImage.height / skinImage.width === 1) {
                //draw down left leg
                context.drawImage(skinImage, 28 * c, 52 * c, 4 * c, 12 * c, 4 * s, 20 * s, 4 * s, 12 * s);
                //draw down left arm
                context.drawImage(skinImage, 44 * c, 52 * c, 4 * c, 12 * c, 0 * s, 8 * s, 4 * s, 12 * s);
            }

            //endBack
        } else if (/avaSkin/.test(classname)) {
            //draw the head
            context.drawImage(skinImage, 8 * c, 8 * c, 8 * c, 8 * c, 0 * s, 0 * s, 15 * s, 15 * s);
            //draw the mask
            context.drawImage(skinImage, 40 * c, 8 * c, 8 * c, 8 * c, 0 * s, 0 * s, 15 * s, 15 * s);
        }
        //we replace the image with the canvas
        skinImage.parentNode.replaceChild(canvas, skinImage);
        //we create a new canvas element
    }

    //helper function for IE
    function getElementsByClassName(className, tag, elm) {
        var testClass = new RegExp("(^|\\s)" + className + "(\\s|$)");
        var tag = tag || "*";
        var elm = elm || document;
        var elements = (tag == "*" && elm.all) ? elm.all : elm.getElementsByTagName(tag);
        var returnElements = [];
        var current;
        var length = elements.length;
        for (var i = 0; i < length; i++) {
            current = elements[i];
            if (testClass.test(current.className)) {
                returnElements.push(current);
            }
        }
        return returnElements;
    }

    //helper function to check canvas support
    function supportsCanvas() {
        canvas_compatible = false;
        try {
            canvas_compatible = !!(document.createElement('canvas').getContext('2d')); // S60
        } catch (e) {
            canvas_compatible = !!(document.createElement('canvas').getContext); // IE
        }
        return canvas_compatible;
    }