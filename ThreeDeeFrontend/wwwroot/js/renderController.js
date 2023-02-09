let stlViewer;
let dotnetRef;

export async function addModel(path, color, id, ref){
    dotnetRef = ref;
    console.log(ref);
    if(stlViewer != null){
        stlViewer.clean();
    }
    let elem = document.getElementById("stl_cont");
    stlViewer = new StlViewer(elem, {
        loading_progress_callback: await loadProgress,
        models: [ {id: id, filename: path, color: color, rotationx: -1} ] 
    });
}

async function loadProgress(load_status, load_session)
{
    let loaded = 0;
    let total = 0;
    Object.keys(load_status).forEach(function(model_id)
    {
        if (load_status[model_id].load_session===load_session)
        {
            loaded+=load_status[model_id].loaded;
            total+=load_status[model_id].total;
        }
    });
    console.log(loaded/total * 100);
    dotnetRef.invokeMethodAsync("ProgressChangedJsCallback", loaded/total*100);
}

export async function changeToColor(color, id){
    stlViewer.set_color(id, color);
}
