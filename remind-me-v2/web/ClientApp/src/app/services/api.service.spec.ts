import { HttpClientModule } from "@angular/common/http";
import { inject, TestBed } from "@angular/core/testing";
import { ApiService } from "./api.service";

describe("ApiService", () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [ApiService]
    });
  });

  it("should be created", inject([ApiService], (service: ApiService) => {
    expect(service).toBeTruthy();
  }));
});
