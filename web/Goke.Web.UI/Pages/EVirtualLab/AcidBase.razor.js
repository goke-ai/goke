


function play(video) {
    if (video) {
        video.play();
    }
}

function stop(video) {
    if (video) {
        video.stop();
        video.currentTime = 0;
    }
}

function pause(video) {
    if (video) {
        video.pause();
    }
}

function jumpToSpecificTime(video, timeInSeconds) {
    if (video) {
        video.currentTime = timeInSeconds;
    }
}

window.gstartTimeInSeconds = 0;
window.gstopTimeInSeconds = 0;
window.gisLoop = true;
export function loop(element, startTimeInSeconds, stopTimeInSeconds, isLoop = true) {

    window.gstartTimeInSeconds = startTimeInSeconds;
    window.gstopTimeInSeconds = stopTimeInSeconds;
    window.gisLoop = isLoop;

    console.log('loop');

    const video = document.querySelector(element);

    play(video);
    jumpToSpecificTime(video, startTimeInSeconds)

    const handleTimeUpdate = () => {
        if (video.currentTime >= window.gstopTimeInSeconds) {
            if (window.gisLoop) {
                jumpToSpecificTime(video, window.gstartTimeInSeconds)
            }
            else {
                console.log("islloop:false");
                pause(video);
            }
        }


    }
    video.addEventListener("timeupdate", handleTimeUpdate);

    console.log('loop end');
}

export function stopLoop(element) {
    const video = document.querySelector(element);
    video.removeEventListener("timeupdate", handleTimeUpdate);
}
