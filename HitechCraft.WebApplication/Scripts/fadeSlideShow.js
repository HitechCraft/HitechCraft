/*
 * fadeSlideShow
 * v.2.0.0
 *
 * Copyright (c) 2010 Pascal Bajorat (http://www.pascal-bajorat.com)
 * Dual licensed under the MIT (below)
 * and GPL (http://www.gnu.org/licenses/gpl.txt) licenses.
 *
 *
 * http://plugins.jquery.com/project/fadeslideshow
 * http://www.pascal-bajorat.com

MIT License

Copyright (c) 2010 Pascal Bajorat

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

jQuery.fn.fadeSlideShow = function(options) {
	return this.each(function(){
		settings = jQuery.extend({
			speed: 'slow', // default animation transition speed
			interval: 3000, // default interval between image change
			PlayPauseElement: 'fssPlayPause', // default css id for the play / pause element
			PlayText: 'Play', // default play text
			PauseText: 'Pause', // default pause text
			NextElement: 'fssNext', // default id for next button
			NextElementText: 'Next >', // default text for next button
			PrevElement: 'fssPrev', // default id for prev button
			PrevElementText: '< Prev', // default text for prev button
			ListElement: 'fssList', // default id for image / content controll list
			ListLi: 'fssLi', // default class for li's in the image / content controll 
			ListLiActive: 'fssActive', // default class for active state in the controll list
			addListToId: false, // add the controll list to special id in your code - default false
			allowKeyboardCtrl: true, // allow keyboard controlls left / right / space
			autoplay: true // autoplay the slideshow
	 	}, options);
		
		// set styles for child element
		jQuery('> *',this).css({
			position: 'absolute'
		});
		
		// count number of slides
		Slides = jQuery('> *', this).length;
		Slides = Slides - 1;
		ActSlide = Slides;
		// Set jQuery Slide short var
		jQslide = jQuery('> *', this);
		// save this
		fssThis = this;
		
		autoplay = function(){
			intval = setInterval(function(){
				jQslide.eq(ActSlide).fadeOut(settings.speed);
				
				// if list is on change the active class
				if(settings.ListElement){
					setActLi = (Slides - ActSlide) + 1;
					if(setActLi > Slides){setActLi=0;}
					jQuery('#'+settings.ListElement+' li').removeClass(settings.ListLiActive);
					jQuery('#' + settings.ListElement + ' li').eq(setActLi).addClass(settings.ListLiActive);

					$('.title-text').text($(jQslide[setActLi]).find('.text').text());
					$('.title-desc').text($(jQslide[setActLi]).find('.desc').text());
				}
				
				if(ActSlide <= 0){
					jQslide.fadeIn(settings.speed);
					ActSlide = Slides;
				}else{
					ActSlide = ActSlide - 1;	
				}
			}, settings.interval);
			
			if(settings.PlayPauseElement){
				jQuery('#'+settings.PlayPauseElement).html(settings.PauseText);
			}
		}
		
		if(settings.allowKeyboardCtrl){
			jQuery(document).bind('keydown', function(e){
				if(e.which==39){
					nextSlide = ActSlide-1;
					stopAutoplay();
					jumpTo(nextSlide);
				}else if(e.which==37){
					prevSlide = ActSlide+1;
					stopAutoplay();
					jumpTo(prevSlide);
				}else if(e.which==32){
					if(intval){stopAutoplay();}
					else{autoplay();}
					return false;
				}
			});
		}
		
		// start autoplay or set it to false
		if(settings.autoplay){autoplay();}else{intval=false;}
	});
};