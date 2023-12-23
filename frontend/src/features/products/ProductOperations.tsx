import { Filter } from "../../ui/Filter";
import { Pagination } from "../../ui/Pagination";
import SortBy from "../../ui/SortBy";
import TableOperations from "../../ui/TableOperations";

export const ProductTableOperations = () => {
  return (
    <TableOperations>
      <Filter
        filterField="filteredBy"
        options={[
          { value: "all", label: "All" },
          { value: "active", label: "Active" },
          { value: "inactive", label: "Inactive" },
        ]}
      />

      <SortBy
        options={[
          { value: "name-asc", label: "Sort by name (A-Z)" },
          { value: "name-desc", label: "Sort by name (Z-A)" },
          { value: "regularPrice-asc", label: "Sort by price (low first)" },
          { value: "regularPrice-desc", label: "Sort by price (high first)" },
          { value: "category-asc", label: "Sort by category (A-Z)" },
          { value: "category-desc", label: "Sort by category (Z-A)" },
        ]}
      />
      <Pagination count={100} />
    </TableOperations>
  );
};
