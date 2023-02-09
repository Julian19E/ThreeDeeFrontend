let stlViewer;

export async function addModel(path, color, id){
    if(stlViewer == null){
        let elem = document.getElementById("stl_cont");
        stlViewer = new StlViewer(elem, { models: [ {id: id, filename: path, color: color, rotationx: -1} ] });
        console.log("instantiated");        
    }
    else{
        stlViewer.add_model({ models: [ {id: id, filename: path, color: color, rotationx: -1} ] });
        console.log("added. path: " + path);
    }
}

export async function changeToColor(color, id){
    stlViewer.set_color(id, color);
}