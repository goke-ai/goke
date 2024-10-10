/// <reference path="assests/typed.umd.js" />

function convertArray(win1251Array) {

    console.log('convertArray');

    var win1251decoder = new TextDecoder('windows-1251');
    var bytes = new Uint8Array(win1251Array);
    var decodedArray = win1251decoder.decode(bytes);
    return decodedArray;

};

function TypedControl(element, 
    /**
   * @property {array} strings strings to be typed
   * @property {string} stringsElement ID of element containing string children
   */
  strings= [
        'These are the default values...',
        'You know what you should do?',
        'Use your own!',
        'Have a great day!',
    ],
    stringsElement= null,

    /**
     * @property {number} typeSpeed type speed in milliseconds
     */
    typeSpeed= 0,

    /**
     * @property {number} startDelay time before typing starts in milliseconds
     */
    startDelay= 0,

    /**
     * @property {number} backSpeed backspacing speed in milliseconds
     */
    backSpeed= 0,

    /**
     * @property {boolean} smartBackspace only backspace what doesn't match the previous string
     */
    smartBackspace= true,

    /**
     * @property {boolean} shuffle shuffle the strings
     */
    shuffle= false,

    /**
     * @property {number} backDelay time before backspacing in milliseconds
     */
    backDelay= 700,

    /**
     * @property {boolean} fadeOut Fade out instead of backspace
     * @property {string} fadeOutClass css class for fade animation
     * @property {boolean} fadeOutDelay Fade out delay in milliseconds
     */
    fadeOut= false,
    fadeOutClass= 'typed-fade-out',
    fadeOutDelay= 500,

    /**
     * @property {boolean} loop loop strings
     * @property {number} loopCount amount of loops
     */
    loop= false,
    loopCount= Infinity,

    /**
     * @property {boolean} showCursor show cursor
     * @property {string} cursorChar character for cursor
     * @property {boolean} autoInsertCss insert CSS for cursor and fadeOut into HTML <head>
     */
    showCursor= true,
    cursorChar= '|',
    autoInsertCss= true,

    /**
     * @property {string} attr attribute for typing
     * Ex= input placeholder, value, or just HTML text
     */
    attr= null,

    /**
     * @property {boolean} bindInputFocusEvents bind to focus and blur if el is text input
     */
    bindInputFocusEvents= false,

    /**
     * @property {string} contentType 'html' or 'null' for plaintext
     */
    contentType= 'html'

    ///**
    // * Before it begins typing
    // * @param {Typed} self
    // */
    //onBegin= (self) => {},

    ///**
    // * All typing is complete
    // * @param {Typed} self
    // */
    //onComplete= (self) => {},

    ///**
    // * Before each string is typed
    // * @param {number} arrayPos
    // * @param {Typed} self
    // */
    //preStringTyped= (arrayPos, self) => {},

    ///**
    // * After each string is typed
    // * @param {number} arrayPos
    // * @param {Typed} self
    // */
    //onStringTyped= (arrayPos, self) => {},

    ///**
    // * During looping, after last string is typed
    // * @param {Typed} self
    // */
    //onLastStringBackspaced= (self) => {},

    ///**
    // * Typing has been stopped
    // * @param {number} arrayPos
    // * @param {Typed} self
    // */
    //onTypingPaused= (arrayPos, self) => {},

    ///**
    // * Typing has been started after being stopped
    // * @param {number} arrayPos
    // * @param {Typed} self
    // */
    //onTypingResumed= (arrayPos, self) => {},

    ///**
    // * After reset
    // * @param {Typed} self
    // */
    //onReset= (self) => {},

    ///**
    // * After stop
    // * @param {number} arrayPos
    // * @param {Typed} self
    // */
    //onStop= (arrayPos, self) => {},

    ///**
    // * After start
    // * @param {number} arrayPos
    // * @param {Typed} self
    // */
    //onStart= (arrayPos, self) => {},

    ///**
    // * After destroy
    // * @param {Typed} self
    // */
    //onDestroy= (self) => {},

) {

    console.log(strings);

    const typed = new Typed(element, {
        strings: strings,
        stringsElement: stringsElement,
        typeSpeed: typeSpeed,
        startDelay: startDelay,
        backSpeed: backSpeed,
        smartBackspace: smartBackspace,
        shuffle: shuffle,
        backDelay: backDelay,
        fadeOut: fadeOut,
        fadeOutClass: fadeOutClass,
        fadeOutDelay: fadeOutDelay,
        loop: loop,
        loopCount: loopCount,
        showCursor: showCursor,
        cursorChar: cursorChar,
        autoInsertCss: autoInsertCss,
        attr: attr,
        bindInputFocusEvents: bindInputFocusEvents,
        contentType: contentType
    });
}

