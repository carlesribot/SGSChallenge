import { useSearchParams } from "react-router-dom";
import { Select, SelectOptions } from "./Select";

export type DropDownProps = {
  options: SelectOptions[];
  id: string;
};

export const DropDown: React.FC<DropDownProps> = ({ options, id }) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const pageSize = searchParams.get(id) || "";

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    searchParams.set(id, e.target.value);
    setSearchParams(searchParams);
  };

  return (
    <Select
      options={options}
      type="white"
      value={pageSize}
      onChange={handleChange}
    />
  );
};
