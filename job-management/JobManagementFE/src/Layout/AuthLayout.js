import React from "react";
import StyleSwitcher from "./CommonLayout/StyleSwitcher";

const AuthLayout = ({ children }) => {
  return <React.Fragment>{children}
  <StyleSwitcher /></React.Fragment>;
};

export default AuthLayout;
