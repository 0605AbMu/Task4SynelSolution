Array.prototype.remove = function(item)
{
    let index = this.indexOf(item);
    if (index > -1)
        this.splice(index, 1);
}

function Sort(paramName) {
    ToogleSortParam(sortingMetadata, paramName);
    let url = new URL(window.location);
        url.searchParams.set("sorting", JSON.stringify(sortingMetadata));
        window.location = url.toString();
}

function ToogleSortParam(sortingMetadata, paramName) {
    if (sortingMetadata.asc.includes(paramName)) {
        sortingMetadata.asc.remove(paramName);
        sortingMetadata.desc.push(paramName);
    } else if (sortingMetadata.desc.includes(paramName)) {
            sortingMetadata.desc.remove(paramName);
            sortingMetadata.asc.push(paramName)
    } else sortingMetadata.asc.push(paramName);
}

function Paging(pageOrIndex){
    if (typeof pageOrIndex == "string")
    {
        if (pageOrIndex === "next")
            paginationMetaData.current++;
        else if (pageOrIndex ==="previous")
            paginationMetaData.current--;
    }
    else if (typeof pageOrIndex == "number")    
        paginationMetaData.current = pageOrIndex;
    console.log(paginationMetaData);
    let url = new URL(window.location);
        url.searchParams.set("pagination", JSON.stringify(paginationMetaData));
    window.location = url.toString();
}


function DeleteEmployeeById(id)
{
    if (window.confirm("Are you sure?"))
        window.location = "/delete/" + id;
}
