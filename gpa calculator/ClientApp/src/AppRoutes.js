import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Calculator } from "./components/Calculator";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
      element: <FetchData />
  },
  {
        path: '/calculator',
        element: <Calculator />
  }

];

export default AppRoutes;
