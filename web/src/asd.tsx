import React from "react";

import unchecked from "./assets/uncheked.png";
function CustomUncheckedIcon() {
  return (
    <img style={{width: 40, height: 40}} src={unchecked}/>

  );
}

function CustomCheckedIcon() {
  return (
    <svg xmlns="http://www.w3.org/2000/svg"  viewBox="0,0,256,256" width="40px" height="40px" fill-rule="nonzero">
      <g transform="">
        <g fill="none" fill-rule="nonzero" stroke="none" stroke-width="1" stroke-linecap="butt" stroke-linejoin="miter" stroke-miterlimit="10" stroke-dasharray="" stroke-dashoffset="0" font-family="none" font-weight="200" font-size="none" text-anchor="inherit">
          <g transform="scale(5.33333,5.33333)">
            <path d="M36,42h-24c-3.314,0 -6,-2.686 -6,-6v-24c0,-3.314 2.686,-6 6,-6h24c3.314,0 6,2.686 6,6v24c0,3.314 -2.686,6 -6,6z" fill="#ffffff"></path>
            <path d="M34.585,14.586l-13.571,13.586l-5.601,-5.588l-2.826,2.832l8.432,8.412l16.396,-16.414z" fill="#1ac621"></path>
          </g>
        </g>
      </g>
    </svg>
  );
}

export { CustomUncheckedIcon, CustomCheckedIcon };
