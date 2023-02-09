let stlViewer;

export async function addModel(path, color, id){
    if(stlViewer != null){
        stlViewer.clean();
    }
    let elem = document.getElementById("stl_cont");
    stlViewer = new StlViewer(elem, { models: [ {id: id, filename: path, color: color, rotationx: -1} ] });
    console.log("instantiated. path: " + path);        
    console.log("path: " +  window.location.href);
}

export async function changeToColor(color, id){
    stlViewer.set_color(id, color);
}
