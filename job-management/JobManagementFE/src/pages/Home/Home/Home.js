import React from "react";
const Section = React.lazy(() => import("../Home/Section"));
const Home = React.lazy(() => import("../Home"));

const Layout1 = () => {
  document.title = "Home | Jobcy - Job Listing Template | Themesdesign";
  return (
    <div>
      <Section />
      <Home />
    </div>
  );
};

export default Layout1;
