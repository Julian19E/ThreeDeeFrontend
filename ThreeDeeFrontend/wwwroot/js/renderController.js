
export async function changeCanvasStyle(r, g, b){
    let canvasParent = document.getElementById("blazorview3d");
    //console.log(canvasParent.children);
    //console.log(canvasParent.childNodes[0]);
    
    let canvas = canvasParent.firstElementChild;
    console.log(canvas);
    let ctx = canvas.getContext("webgl2");
    console.log(r);
    console.log(g);
    console.log(b);
    ctx.clearColor(r, g, b, 1);
    ctx.clear(ctx.COLOR_BUFFER_BIT);
    //ctx.globalCompositeOperation = 'destination-over'
    //ctx.fillStyle = "#000000";
    //ctx.fillRect(0, 0, canvas.width, canvas.height);   
    
}