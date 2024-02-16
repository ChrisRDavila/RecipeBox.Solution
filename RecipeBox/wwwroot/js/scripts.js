function expandNavBarItem(elementId) {
  document.getElementById(elementId).classList.toggle("active");
  const content = document.getElementById(elementId).nextElementSibling;
  if (content.style.maxHeight) {
    content.style.maxHeight = null;
  } else {
    content.style.maxHeight = content.scrollHeight + "px";
  }
}
