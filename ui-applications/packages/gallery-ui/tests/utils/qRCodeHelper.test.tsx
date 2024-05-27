import { expect, test } from 'vitest';
import { saveQRCode, getQRCodeUrl } from "../../src/utils/qRCodeHelper";
import { LocalStorageKeys } from "../../src/utils/constants"

/**
* @vitest-environment jsdom
*/
test("QR code url must be saved and retrieved successfuly", () => {
    saveQRCode("qRCodeUrl");
    expect(getQRCodeUrl()).toBe("qRCodeUrl");
  });

test("QR code url must be saved and retrieved from local storage", () => {
    saveQRCode("qRCodeUrl");
    expect(getQRCodeUrl()).toBe(localStorage.getItem(LocalStorageKeys.QRCodeUrl));
})