import { useSearchParams } from "react-router-dom";
import Select from "./Select";

function PageSize({ options }) {
  const [searchParams, setSearchParams] = useSearchParams();
  const pageSize = searchParams.get("pageSize") || "";

  function handleChange(e) {
    searchParams.set("pageSize", e.target.value);
    setSearchParams(searchParams);
  }

  return (
    <Select
      options={options}
      type="white"
      value={pageSize}
      onChange={handleChange}
    />
  );
}

export default PageSize;
