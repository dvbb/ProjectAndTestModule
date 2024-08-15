let video = document.getElementById("videoId");
function play(){
    //视频元素可以选择静音后再播放,提示用户打开声音
    video.muted=true;//打开静音
    video.play();

    //判断当前是否可以用播放声音
    const ctx = new AudioContext();
    const canAutoPlay = ctx.state === 'running';//能播放声音返回running
    ctx.close();

    if(canAutoPlay){
        video.muted = false;//取消静音
    }
}
play();