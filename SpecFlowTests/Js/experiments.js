//locate any element usong CSS or XPATH
const any$ = (cssOrXpath, parent = document) => {
    return /^[\.\(]*\/.*/.test(cssOrXpath)
        ? document.evaluate(cssOrXpath.replace(/^\//, "./"), parent, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue
        : parent.querySelector(cssOrXpath);
}

const rectContains = (rect, x, y) => x > rect.left && x < rect.right && y > rect.top && y < rect.bottom


/**
 * This will fire event once per 300ms
 * e.g. document.onmousemove = throttle(event => console.log("hello"), 300)
 */
function throttle(callback, wait) {
    var timeout
    return function(e) {
        if (timeout) return;
        timeout = setTimeout(() => (callback(e), timeout=undefined), wait)
    }
}
function getCommonParentNode(node1, node2) {
    if (node1 == node2) return node1;
    var parent = node1;
    do if (parent.contains(node2)) return parent
    while (parent = parent.parentNode);
    return null;
}

//search label under the mouse
//search component(label) in document
//search first parent of label+component starting from component

//if(mouseOverParent) addBorders(label, component, parent)
function highlightComponent(labelSelector, componentFinder) {
    document.onmousemove = throttle(e => {
        const found = document.elementsFromPoint(e.clientX, e.clientY)[1]
        if (!e.ctrlKey) return
        //foreach
        const labelEl= any$(labelSelector, found)
        if(!labelEl) {console.log("1"); return;}
        const component = componentFinder(labelEl.textContent);
        if(!component)  {console.log("2"); return;}
        const parent = getCommonParentNode(component, labelEl)
        if (!rectContains(parent.getBoundingClientRect(), e.clientX, e.clientY))  {console.log("3"); return;}

        labelEl.style.border = "1px solid green"
        component.style.border = "1px solid blue"
        parent.style.border = "1px solid red"
        parent.onmouseout = function (e) {
            labelEl.style.border = null
            component.style.border = null
            parent.style.border = null
            
            this.onmouseout = null;
        }
    },300)
}
highlightComponent("//label", lab => any$(`//label[.='${lab}']/../..//input`))


document.onmousemove = e => console.log(document.elementsFromPoint(e.clientX, e.clientY))


el = document.querySelectorAll(':hover'); // array z góry na dół -> get last
document.onmousemove = throttle(e => console.log(document.elementsFromPoint(e.clientX, e.clientY)[0]), 500) //array z dołu do góry